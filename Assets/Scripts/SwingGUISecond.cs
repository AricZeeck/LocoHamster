using UnityEngine;
using System.Collections;

public class SwingGUISecond : MonoBehaviour
{
    // POWER VARIABLES
    public int maxSwingPower = 100;
    public int currentSwingPower = 1;
    public float swingPowerBarLength;
    public bool swingPowerIncrease;
    public bool finishedSwingPower;
    public float desiredPower = 500;
    public float maxDesiredPower = 100;

    // CURVE VARIBALES
    public int maxSwingCurve = 50;
    public float swingCurveBarLength = 0;
    public bool finalSwingCurve;
    public float desiredCurve;
    public int currentSwingCurve = 1;
    public bool swingCurveIncrease;

    // OTHER VARIBALES    
    public bool inPreSwing;
    public bool inSwingPower;
    public bool inSwingCurve;
    public bool inPostSwing;    

    // Use this for initialization
    void Start()
    {
        inPreSwing = true;
        inSwingPower = false;
        inSwingCurve = false;
        inPostSwing = false;
        swingPowerIncrease = false;
        swingCurveIncrease = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPreSwing)
        {
            // Add code to set desired power and curve
            if (Input.GetKeyUp(KeyCode.Space))
            {
                inPreSwing = false;
                inSwingPower = true;
                swingPowerIncrease = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                desiredPower++;
                if (desiredPower >= Screen.width * .5)
                {
                    desiredPower = (float)(Screen.width * .5);
                }
                
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                desiredPower--;
                if (desiredPower <= 0)
                {
                    desiredPower = 0;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                desiredCurve++;
                if (desiredCurve >= 50)
                {
                    desiredCurve = 50;
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                desiredCurve--;
                if (desiredCurve <= -50)
                {
                    desiredCurve = -50;
                }
            }
        }

        if (inSwingPower)
        {
            if (Input.GetKey(KeyCode.Space))
            {                
                print("Swinging");
                swingPowerIncrease = false;
                inSwingPower = false;
                inSwingCurve = true;
            }

            if (swingPowerIncrease)
            {        
                IncreaseSwingPower(0);                
            }

            if (!swingPowerIncrease)
            {
                DecreaseSwingPower(0);                
            }            
        }

        if (inSwingCurve)
        {

        }

        if (inPostSwing)
        {
            print("In Post Swing");
            print("Your swing power is " + currentSwingPower);
            print("Your swing curve right is " + currentSwingCurve);
        }        

        swingPowerBarLength = Screen.width * (currentSwingPower / (float)maxSwingPower);
        print(currentSwingPower);
    }

    void OnGUI()
    {
        GUI.contentColor = Color.green;        

        // GUI bars

        GUI.Box(new Rect(10, 10, Screen.width , 20), "");
        GUI.Box(new Rect(10, 10, swingPowerBarLength, 20),
            currentSwingPower + " / " + maxSwingPower);        

        GUI.Box(new Rect(10, 50, 20, swingCurveBarLength),
            currentSwingCurve + "/" + maxSwingCurve);

        // Desired Power/Curve
        GUI.Box(new Rect(desiredPower + 10, 10, 40, 20),
            desiredPower.ToString());

        GUI.Box(new Rect(10, desiredCurve + 50, 20, 40),
            desiredCurve.ToString());
    }

    public void IncreaseSwingPower(int adj)
    {        
        currentSwingPower += adj;
        currentSwingPower++;

        if (currentSwingPower >= maxSwingPower)
        {
            currentSwingPower = maxSwingPower;
            swingPowerIncrease = false;
        }
    }

    public void DecreaseSwingPower(int adj)
    {
        print("In Increase Power method");
        currentSwingPower--;

        if (currentSwingPower <= 0)
        {
            currentSwingPower = 0;
            swingPowerIncrease = true;
        }
    }

    public void IncreaseCurveRight(int adj)
    {

    }

    public void DecreaseCurveRight(int adj)
    {

    }
}