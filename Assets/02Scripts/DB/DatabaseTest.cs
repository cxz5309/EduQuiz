using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // Orderby

public class DatabaseTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //해당 테이블의 id로 데이터 받아오기
        var testData = DataService.Instance.GetData<Table.Data>(1);
        Debug.Log("Data id :" + testData.id);
        Debug.Log("Data gold :" + testData.gold);
        Debug.Log("Data now_weapon :" + testData.now_weapon);
        Debug.Log("Data available_weapon :" + testData.available_weapon);

        //해당 테이블의 모든 데이터 받아오기
        var testDataList = DataService.Instance.GetDataList<Table.Data>();
        for (int i = 0; i < testDataList.Count; i++)
        {
            Debug.Log("name :" + testDataList[i].id + ", gold :" + testDataList[i].gold + ", weapon :" + testDataList[i].available_weapon + ", housing :" + testDataList[i].available_housing);
        }

        //    //받아온 테이블 리스트에서 조건 검색: 나이가 30살인 데이터 찾기
        //   var testData = DataService.Instance.GetDataList<Table.TestTable>().Find(x => x.age == 30);
        //    Debug.Log("name :" + testData.name + ", age :" + testData.age);

        //    //받아온 테이블 리스트에서 조건 검색: 나이가 30살이거나 이름이 kim 인 데이터 찾기
        //    var testDataList = DataService.Instance.GetDataList<Table.TestTable>().FindAll(x => x.age == 30 || x.name == "kim");
        //    for (int i = 0; i < testDataList.Count; i++)
        //    {
        //        Debug.Log("name :" + testDataList[i].name + ", age :" + testDataList[i].age);
        //    }

        //    //받아온 테이블 리스트 정렬(오름차순 )
        //    var testDataList = DataService.Instance.GetDataList<Table.TestTable>().OrderBy(x => x.id).ToList();
        //    //받아온 테이블 리스트 정렬(내림차순 )
        //    var testDataList = DataService.Instance.GetDataList<Table.TestTable>().OrderByDescending(x => x.id).ToList();
        //    for (int i = 0; i < testDataList.Count; i++)
        //    {
        //        Debug.Log("name :" + testDataList[i].name + ", age :" + testDataList[i].age);
        //    }

        //    //테이블 값 수정하기
        //   var testData = DataService.Instance.GetData<Table.TestTable>(2);
        //    testData.name = "kang";
        //    int result = DataService.Instance.UpdateData<Table.TestTable>(testData);
        //    Debug.Log("result:" + result);

        //    //테이블에 값 추가
        //    var testData = new Table.TestTable();
        //    testData.name = "go";
        //    testData.age = 40;
        //    DataService.Instance.InsertData(testData);

        //    //테이블 데이터 삭제
        //    DataService.Instance.DeleteData<Table.TestTable>(0);

        //    //나이가 35살 이하인 사람들의 이름을 Jo 로 바꾸기
        //   var testDataList = DataService.Instance.GetDataList<Table.TestTable>().FindAll(x => x.age <= 35);
        //    for (int i = 0; i < testDataList.Count; i++)
        //    {
        //        testDataList[i].name = "jo";
        //    }
        //    DataService.Instance.UpdateDataAll(testDataList);

        //    //이름이 Jo 인 데이터들을 싹 지우기
        //    var testDataList = DataService.Instance.GetDataList<Table.TestTable>().FindAll(x => x.name == "jo");
        //    DataService.Instance.DeleteDataAll(testDataList);
    }
}
