using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;    // Preserve Attribute
using SQLite.Attribute;
using System;   // Serializable

public class Table
{
    // 읽기전용 테이블 배열
    public static Type[] readOnlyTableTypeArray = new Type[]
    {
    };

    // 읽기/쓰기 전용 테이블 배열
    public static Type[] writableTableTypeArray = new Type[]
    {
        typeof(Data)
    };

    //#region ReadOnlyTable
    //[Preserve, Serializable]
    //public class DataTable : IKeyTableData
    //{
    //    public int GetTableId() { return id; }
    //    [NotNull, PrimaryKey, Unique]
    //    public int id { get; set; }
    //    public int gold { get; set; }
    //    public int now_weapon { get; set; }
    //    public string available_weapon { get; set; }
    //    public string available_housing { get; set; }
    //}
    //#endregion

    #region WritableTable
    // Preserve : 유니티가 빌드를 할 때 해당 클래스가 사용되고 있지 않더라도 빌드에 포함시켜줌
    [Preserve, Serializable]
    public class Data : IKeyTableData, IInsertableTableData
    {
        public void SetTableId(int id) { this.id = id;  }
        public static int max;
        [Ignore]
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        public int GetTableId() { return id; }
        [NotNull, PrimaryKey, Unique]
        public int id { get; set; }
        public int gold { get; set; }
        public int now_weapon { get; set; }
        public string available_weapon { get; set; }
        public string available_housing { get; set; }
    }
    #endregion



}
