using UnityEngine;
using TMPro;
public class NumberLineGenerator : MonoBehaviour
{
    public GameObject numberPrefab;
    public int numberOfNumbers = 10;
    public float spacing = 1.0f;
    public Transform parentTransform; // Specify the parent GameObject in the Inspector

    public Color numberColor;
    public Color circleColor;

    void Start()
    {
        GenerateNumberLine();
    }

    void GenerateNumberLine()
    {
        if (parentTransform == null)
        {
            Debug.LogError("Parent transform is not assigned.");
            return;
        }

        for (int i = numberOfNumbers * -1; i <= numberOfNumbers; i++)
        {
            // Instantiate a number object
            GameObject numberObject = Instantiate(numberPrefab);

            // Set the parent of the instantiated object
            numberObject.transform.SetParent(parentTransform);
            
            // Change Number font color
            numberObject.GetComponent<TMP_Text>().color = numberColor;   

            //Change background circle color
            Transform bgCircle = numberObject.transform.Find("Circle");
            if(bgCircle != null)
            {
                bgCircle.GetComponent<SpriteRenderer>().color = circleColor;
            }
            else
            {
                Debug.Log("NumberLineGenerator.cd : Background Circle Not Found. ");
            }
            if(i == 0)
            {
                // Position the number object in a line
                numberObject.transform.localPosition = Vector3.right * i * spacing + new Vector3(-0.19f, -0.18f, 0);
            }
            else
            {
                // Position the number object in a line
                numberObject.transform.localPosition = Vector3.right * i * spacing + new Vector3(0, -0.18f, 0);
            }
            numberObject.GetComponent<TextMeshPro>().text = i.ToString();
        }


        for (int i = numberOfNumbers * -1; i <= numberOfNumbers; i++)
        {

            if(i != 0)
            { 
                // Instantiate a number object
                GameObject numberObject = Instantiate(numberPrefab);

                // Set the parent of the instantiated object
                numberObject.transform.SetParent(parentTransform);
                
                // Change Number font color
                numberObject.GetComponent<TMP_Text>().color = numberColor;   

                //Change background circle color
                Transform bgCircle = numberObject.transform.Find("Circle");
                if(bgCircle != null)
                {
                    bgCircle.GetComponent<SpriteRenderer>().color = circleColor;
                }
                else
                {
                    Debug.Log("NumberLineGenerator.cd : Background Circle Not Found. ");
                }

                if(i < 0)
                {
                    bgCircle.localScale = new Vector3(0.27f, bgCircle.localScale.y, bgCircle.localScale.z);
                }
                float offset = 0;
                if(i < -9)
                {
                    offset = 0.06f;
                    bgCircle.localScale = new Vector3(0.38f, bgCircle.localScale.y, bgCircle.localScale.z);
                }
                if(i > 9)
                {
                    offset = 0.06f;
                    bgCircle.localScale = new Vector3(0.38f, bgCircle.localScale.y, bgCircle.localScale.z);
                }
                    
                
                // Calculate the position with Y-axis offset
                Vector3 position =  Vector3.up * i * spacing + new Vector3(-0.19f - offset, 0, 0);

                // Position the number object
                numberObject.transform.localPosition = position;
                numberObject.GetComponent<TextMeshPro>().text = i.ToString();
            }
        }
    }
}
