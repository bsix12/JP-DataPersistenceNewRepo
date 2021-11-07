using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public static DataStorage Instance;

    public int highScoreData;
    public string playerInputData;

    private void Awake()                    // awake is called as soon as the object is created
    {
        // this pattern is a singleton
        if (Instance != null)               // the first DataStorage is created when there are none
        {
            Destroy(gameObject);            // any additional DataStorage instances are destroyed if one already exists
            return;
        }
        Instance = this;                    // 'this' is stored in the class member Instance, the current instance of DataStorage
                                            // DataStorage.Instance can now be called from any other script.  Don't need to reference it.
        DontDestroyOnLoad(gameObject);      // marked to persist across scene changes
    }    

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
