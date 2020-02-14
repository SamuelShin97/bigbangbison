using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPlayer1 : MonoBehaviour
{
    public GameObject[] models;
    private Renderer model; // currently enabled model
    public GameObject cursor;
    private float[] cursorX;
    private float[] cursorY;
    private int currentCursor;
    private int currentModel;
    private int newModel;
    private int newCursor;

    // Start is called before the first frame update
    void Start()
    {
        currentModel = 0;
        // Disable all model Renderers
        for(int i = 0; i < 4; i++)
        {
            model = models[i].GetComponent<Renderer>();
            model.enabled = false;
        }
        cursorX = new float[4];
        cursorX[0] = -164.1f;
        cursorX[1] = 9.7f;
        cursorX[2] = -164.1f;
        cursorX[3] = 9.7f;
        cursorY = new float[4];
        cursorY[0] = 70.7f;
        cursorY[1] = 70.7f;
        cursorY[2] = 15.8f;
        cursorY[3] = 15.8f;

        // Enable 1st model Renderer
        model = models[0].GetComponent<Renderer>();
        model.enabled = true;

        // Cursor setup
        currentCursor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)){
            print("right character");
            newModel = Mathf.Abs(currentModel + 1) % 4;
            newCursor = (currentCursor++) % 4;
            change_model();
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            print("left character");
            if (currentModel == 0)
            {
                newModel = 3;
                newCursor = 3;
            }
            else
            {
                newModel = currentModel--;
                newCursor = currentCursor--; 
            }
                change_model();
        }
    }

    void change_model()
    {
        // Get Rendere components of old and new models
        Renderer modelA = models[currentModel].GetComponent<Renderer>();
        Renderer modelB = models[newModel].GetComponent<Renderer>();
        
        // Disable old model and enable new
        modelA.enabled = false;
        modelB.enabled = true;

        // Move cursor to selected icon
        print(cursorX[newCursor]);
        print(cursorY[newCursor]);
        cursor.transform.localPosition = new Vector3(cursorX[newCursor], cursorY[newCursor], 0); ;
        currentCursor = newCursor;

        //Set current model to the newly enabled one
        currentModel = newModel;
    }
}
