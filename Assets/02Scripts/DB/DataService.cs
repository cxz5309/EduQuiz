using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    // File
using SqlCipher4Unity3D;
using System;   // Type
using System.Linq;  // ToList()
using System.Reflection;    // MethodInfo

public class DataService 
{
    private static DataService instance;
    public static DataService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataService();
            }
            return instance;
        }
    }

    public string databaseName = "EduQuizDatabase.db";

    private SQLiteConnection connection;
    private Dictionary<Type, Dictionary<int, IKeyTableData>> tableDic;

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)] // Awake 와 Start 사이에 호출 해줌
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]  // Awake 보다 먼저 호출 해줌
    static void InitDataService ()
    {
        DataService.Instance.InitDB();
        DataService.Instance.InitDataForPlay();
    }

    public void InitDataForPlay ()
    {
        if (connection == null)
        {
            connection = new SQLiteConnection(databaseName, "0000");
        }
        tableDic = new Dictionary<Type, Dictionary<int, IKeyTableData>>();

        for (int i = 0; i < Table.readOnlyTableTypeArray.Length; i++)
        {
            tableDic.Add( Table.readOnlyTableTypeArray[i], new Dictionary<int, IKeyTableData>() );
        }

        for (int i = 0; i < Table.writableTableTypeArray.Length; i++)
        {
            tableDic.Add( Table.writableTableTypeArray[i], new Dictionary<int, IKeyTableData>() );
        }
        Debug.Log("테이블 개수 : " +tableDic.Count);

        // LoadData<T>() 호출
        // Generic함수를 타입 변수를 사용해서 호출하는 방법
        MethodInfo loadDataMethod = this.GetType().GetMethod("LoadData", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var type in tableDic.Keys)
        {
            //LoadData<Table.Data>();
            MethodInfo loadDataGenericMethod = loadDataMethod.MakeGenericMethod(type);  // 
            loadDataGenericMethod.Invoke(this, new object[] { });
        }
    }

    public void LoadData<T> () where T : IKeyTableData, new ()
    {
        var tableDataList = connection.Table<T>().ToList(); // Database 에서 값을 빼서 해당 Table 클래스의 리스트로 리턴
        for (int i = 0; i < tableDataList.Count; i++)
        {
            tableDic[typeof(T)].Add(tableDataList[i].GetTableId(), tableDataList[i]);
        }

        T table = new T();
        IInsertableTableData insertable = table as IInsertableTableData;
        if (insertable != null)
        {
            if (tableDic[typeof(T)].Count > 0 )
            {
                insertable.Max = tableDic[typeof(T)].Keys.Max();
            }
            else
            {
                insertable.Max = -1;
            }
        }
    }

    /// <summary>
    /// 해당 테이블의 id 줄의 데이터를 리턴
    /// </summary>
    public T GetData<T>(int id) where T : IKeyTableData
    {
        Dictionary<int, IKeyTableData> dataDic = tableDic[typeof(T)];
        if (dataDic.ContainsKey(id))
        {
            return (T)dataDic[id];
        }
        Debug.LogError("GetData Error : No Data " + typeof(T).ToString() + ", id :" + id);
        return default(T);  // 빈 테이블을 넘겨줌
    }

    /// <summary>
    /// 해당 테이블의 모든 데이터를 리스트로 리턴
    /// </summary>
    public List<T> GetDataList<T>() where T : IKeyTableData
    {
        if (tableDic.ContainsKey(typeof(T)))
        {
            return tableDic[typeof(T)].Values.Cast<T>().ToList();
        }
        Debug.LogError("GetDataList Error : No Table :" + typeof(T).ToString());
        return default(List<T>);
    }

    /// <summary>
    /// 테이블 값 수정하기. 성공하면 리턴 1. 실패하면 리턴 0.
    /// </summary>
    public int UpdateData<T>(T table) where T : IKeyTableData
    {
        tableDic[typeof(T)][table.GetTableId()] = table;
        return connection.Update(table);
    }

    /// <summary>
    /// 테이블 값 넣기. 리턴 값은 추가된 데이터의 Id
    /// </summary>
    public int InsertData<T>(T table) where T : IKeyTableData, IInsertableTableData
    {
        table.Max++;
        int nextId = table.Max;
        table.SetTableId(nextId);
        tableDic[typeof(T)].Add(nextId, table);
        connection.Insert(table);
        return nextId;
    }

    /// <summary>
    /// 테이블 값 삭제
    /// </summary>
    public void DeleteData<T>(int id) where T : IKeyTableData
    {
        tableDic[typeof(T)].Remove(id);
        connection.Delete<T>(id);        
    }

    /// <summary>
    /// 테이블 데이터들을 한번에 수정하기
    /// </summary>
    public void UpdateDataAll<T> (List<T> tableList) where T: IKeyTableData
    {
        for (int i = 0; i < tableList.Count; i++)
        {
            tableDic[typeof(T)][tableList[i].GetTableId()] = tableList[i];
        }
        connection.UpdateAll(tableList);
    }

    /// <summary>
    /// 테이블 데이터들을 한번에 삭제하기
    /// </summary>
    public void DeleteDataAll <T> (List<T> tableList) where T : IKeyTableData
    {
        for (int i = 0; i < tableList.Count; i++)
        {
            tableDic[typeof(T)].Remove(tableList[i].GetTableId());
        }
        connection.DeleteAll<T>();
    }
    

    public void InitDB ()
    {
        string streamingAssetsPath = string.Empty;
#if UNITY_EDITOR
        streamingAssetsPath = string.Format("Assets/StreamingAssets/{0}", databaseName);
        databaseName = string.Format("Assets/{0}", databaseName);

#elif UNITY_ANDROID || UNITY_IOS
        streamingAssetsPath = string.Format("{0}/{1}", Application.streamingAssetsPath, databaseName);
        databaseName = string.Format("{0}/{1}",Application.persistentDataPath, databaseName);
#endif

        if (!File.Exists(databaseName)) // 첫 실행을 의미
        {
#if UNITY_EDITOR || UNITY_IOS
            File.Copy(streamingAssetsPath, databaseName);
#elif UNITY_ANDROID
            WWW loadDb = new WWW(streamingAssetsPath);
            while (loadDb.isDone == false) {}
            File.WriteAllBytes(databaseName, loadDb.bytes);
            loadDb.Dispose();
            loadDb = null;
#endif
        }
    }
}
