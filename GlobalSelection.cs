using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSelection : MonoBehaviour
{
    SelectedDictionary selected_table;
    RaycastHit hit;

    bool dragSelect;

    Vector3 p1;
    Vector3 p2;

    void Start()
    {
        selected_table = GetComponent<SelectedDictionary>();
        dragSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            p1 = Input.mousePosition;
        }

        if (Input.GetMouseButtonDown(1))
        {
            selected_table.deselectAll();
        }
        if(Input.GetMouseButton(0)){
            if((p1 - Input.mousePosition).magnitude > 40){
                dragSelect = true;
            }
            
        }

        if(Input.GetMouseButtonUp(0)){
            if(dragSelect == false){
                Ray ray = Camera.main.ScreenPointToRay(p1);
                
                if(Physics.Raycast(ray, out hit, 500000f)){
                    if(Input.GetKey(KeyCode.LeftShift)){
                        selected_table.addSelected(hit.transform.gameObject);
                    }else{
                        int id = hit.transform.gameObject.GetInstanceID();
                        
                        if (!(selected_table.CheckContains(id)))
                        {
                            selected_table.deselectAll();
                            selected_table.addSelected(hit.transform.gameObject);
                        }
                        
                    }
                }
            }
        }

        p2 = Input.mousePosition;
        
    }
}
