using UnityEngine;
using TMPro;

public class WurzelBalken : MonoBehaviour
{
    public TextMeshProUGUI ausgabe;
    public GameObject rootBar;    
    public GameObject rootBarStart;
    public GameObject rootBeginning;
    public GameObject rootBeginningStart;

    public Vector3 startingPoint;
    public Vector3 endingPoint;
    private int bracketCount = 0;
    public int rootCount;
    private string lastText;
    public Color color;

    void Update()
    {
        if (lastText != ausgabe.text && ausgabe.text != "")
        {
            lastText = ausgabe.text;
            ausgabe.ForceMeshUpdate();
            CheckForRemoval();

            int r = rootCount;
            bracketCount = 0;
            for (int i = 0; i < ausgabe.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = ausgabe.textInfo.characterInfo[i];

                if (c.character == '√')
                {
                    color = c.color;
                    startingPoint = c.topRight;
                    r--;
                }
                if (r == 0)
                {
                    if (c.character == '(') bracketCount += 1;
                    else if (c.character == ')') bracketCount -= 1;

                    if ((c.character == ' ' && bracketCount == 0) || bracketCount < 0)
                    {
                        endingPoint = c.topLeft;
                        UpdateRoot(startingPoint, endingPoint);
                        return;
                    }
                }
            }
            endingPoint = ausgabe.textInfo.characterInfo[ausgabe.textInfo.characterCount-1].topRight;
        }
        UpdateRoot(startingPoint, endingPoint);
        CheckForRemoval();
    }

    public void CheckForRemoval()
    {
        int r = rootCount;
        foreach (char c in ausgabe.text)
        {if (c == '√') r --;}

        if (r > 0) {
            if(Generaldata.calculationIsRunning)
            color.a = 0;
            else
            Destroy(gameObject);
        }
    }
    
    void UpdateRoot(Vector3 start, Vector3 end)
    {
        Color newColor = color;
        newColor.a += ausgabe.color.a-1;
        if (startingPoint == endingPoint) newColor.a = 0;

        float scale = startingPoint.y/13.29427f;
        
        Vector3 rootPos = new Vector3(startingPoint.x - 1.5f*scale, startingPoint.y*1.2f, 0);
        Vector3 beginningPos = new Vector3(startingPoint.x - scale, startingPoint.y*1.2f + 0.35f * scale, 0);

        rootBarStart.transform.position = ausgabe.transform.TransformPoint(rootPos);
        rootBarStart.transform.localScale = new Vector3(ausgabe.transform.TransformPoint(endingPoint).x -1 
        - ausgabe.transform.TransformPoint(startingPoint).x + 1.5f*scale, 0.75f, 0);

        rootBeginningStart.transform.position = ausgabe.transform.TransformPoint(beginningPos);
        rootBeginningStart.transform.localScale = new Vector3(3*scale, 0.75f, 0);


        rootBar.GetComponent<SpriteRenderer>().color = newColor;
        rootBeginning.GetComponent<SpriteRenderer>().color = newColor;
    }
    
}
