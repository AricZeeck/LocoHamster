using UnityEngine;
using System.Collections;

public class SwingGUI : MonoBehaviour {
	public int maxSwingPower = 100;
    public int currentSwingPower = 50;
    public bool isSwinging = false;
    public bool beganSwing = false;
	public float swingPowerBarLength;

	// Use this for initialization
	void Start () {            
        isSwinging = false;
        beganSwing = false;
	}
	
	// Update is called once per frame
    void Update()
    {                  
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Space pressed");
                isSwinging = true;
                beganSwing = true;
                AdjustSwingPower(0);
                print("back in update");
            }
        
        swingPowerBarLength = Screen.width * (currentSwingPower / (float)maxSwingPower);
        print(currentSwingPower);
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, swingPowerBarLength, 20),
            currentSwingPower + " / " + maxSwingPower);
    }

    public void AdjustSwingPower(int adj)
    {
        bool swingPowerIncrease = true;
        print("In adjustment method");
        if (isSwinging) 
        {
            currentSwingPower += adj;

            if (swingPowerIncrease)
            {
                print("swing power increasing");
                currentSwingPower++;
                //grab a temp instance of currentswingpower (float tempSwingPower = currentSwingPower)
                //tempSwingPower = (currentSwingPower + 1) * Time.deltatime
                //currentSwingPower = tempSwingPower

                if (currentSwingPower >= maxSwingPower)
                {
                    currentSwingPower = maxSwingPower;                    
                    swingPowerIncrease = false;
                    
                }

                /*if (Input.GetKeyDown(KeyCode.Space))
                {
                    isSwinging = false;                   
                    
                } */       
            }

            if (swingPowerIncrease == false)
            {
                print("swing power decreasing");
                currentSwingPower--;

                if (currentSwingPower == 0)
                {
                    currentSwingPower = 1;
                    swingPowerIncrease = true;
                                  
                }                

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isSwinging = false;                   
                    
                }               
            }
            if (isSwinging == false)
            {
                
            }
            
        }
        
        
    }    
}


