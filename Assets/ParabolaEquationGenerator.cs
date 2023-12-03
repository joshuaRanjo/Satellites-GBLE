using UnityEngine;

public class ParabolaEquationGenerator : MonoBehaviour
{
    // Variables for the coefficients of the standard general form
    private float a, b, c, h, k;
    private int decimalPlaces = 1;
    private bool isVertical = true;
    private void Start()
    {
        /*
        a = 1;
        b = 3;
        c = 4;
        
        // Generate random values for a, b, and c
        a = Random.Range(1f, 5f); // Adjust the range as needed
        b = Random.Range(-10f, 10f); // Adjust the range as needed
        c = Random.Range(-20f, 20f); // Adjust the range as needed
        

        // Calculate the vertex form coefficients (h and k)
        h = -b / (2 * a);
        k = c -( (b * b) / (4 * a));

        // Output both forms of the equation
        Debug.Log("Standard General Form: y = " + a + "x^2 + " + b + "x + " + c);
        Debug.Log("Vertex Form: y = " + a + "(x - " + h + ")^2 + " + k);
        */
    }

    // generates a Standard General Form equation then its corresponding vertex form
    public void GenerateStandardEquation() 
    {
        // Generate random values for a, b, and c
        
        a = Random.Range(1f, 5f); // Adjust the range as needed
        b = Random.Range(-10f, 10f); // Adjust the range as needed
        c = Random.Range(-20f, 20f); // Adjust the range as needed

        a = Mathf.Round(a * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
        b = Mathf.Round(b * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
        c = Mathf.Round(c * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
        
        // Calculate the vertex form coefficients (h and k)
        h = -b / (2 * a);
        k = c -( (b * b) / (4 * a));

        // Output both forms of the equation
        //Debug.Log("Standard General Form: y = " + a + "x^2 + " + b + "x + " + c);
        //Debug.Log("Vertex Form: y = " + a + "(x - " + h + ")^2 + " + k);
    }

    public string GenerateVertexEquation()
    {
        isVertical = Random.Range(0,2) == 1;
        

        a = Random.Range(-5, 5); // Adjust the range as needed

        //ensure constant a is not zero

        while(a == 0)
            a = Random.Range(-5,5);

        h = Random.Range(-10, 10); // Adjust the range as needed
        k = Random.Range(-5, 5); // Adjust the range as needed

        // Calculate the standard general form coefficients (a, b, and c)
        float b = -2 * a * h;
        float c = a * h * h + k;

        // Output both forms of the equation

        string stringA = a.ToString();
        string stringH = h.ToString();
        string stringK = k.ToString();

        if(a < 0) { stringA = "(" + a.ToString() + ")"; }
        if(h < 0) { stringH = "(" + h.ToString() + ")"; }
        if(k < 0) { stringK = "(" + k.ToString() + ")"; }
        

        //Debug.Log("Vertex Form: y = " + a + "(x - " + h + ")^2 + " + k);
        //Debug.Log("Standard General Form: y = " + a + "x^2 + " + b + "x + " + c);

        string vertexEquation;

        if(isVertical)
        {
            vertexEquation = "(y - " + stringK + ") = " + stringA + "(x - " + stringH + ")^2  " ;
        }
        else
        {
            vertexEquation = "(x - " + stringH + ") = " + stringA + "(y - " + stringK + ")^2  " ;
        }
            

        return vertexEquation; 

       
    }

    //Getters
    public float valueH
    {
        get
        {
            return h;
        }
    }
    public float valueK
    {
        get
        {
            return k;
        }
    }

    public float valueA
    {
        get
        {
            return a;
        }
    }

    public bool valueIsVertical
    {
        get
        {
            return isVertical;
        }
    }
}