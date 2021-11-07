using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugeUI : MonoBehaviour
{
    public Jauge reactionaryJauge;
    public Jauge progressiveJauge;
    public Jauge masqueradeJauge;
    public Jauge boonJauge;
    public Jauge anarchsJauge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int reactionary
    {
        set { reactionaryJauge.value = value; }
    }
    public void triggerReactionary(){
        reactionaryJauge.triggerColor();
    }

    public int progressive
    {
        set { progressiveJauge.value = value; }
    }
    public void triggerProgressive(){
        progressiveJauge.triggerColor();
    }
    public int masquerade
    {
        set { masqueradeJauge.value = value; }
    }
    public void triggerMasquerade(){
        masqueradeJauge.triggerColor();
    }
    public int boon
    {
        set { boonJauge.value = value; }
    }
    public void triggerBoon(){
        boonJauge.triggerColor();
    }
    public int anarch
    {
        set { anarchsJauge.value = value; }
    }
    public void triggerAnarchs(){
        anarchsJauge.triggerColor();
    }

    public void resetColors(){
        reactionaryJauge.resetColor();
        progressiveJauge.resetColor();
        masqueradeJauge.resetColor();
        boonJauge.resetColor();
        anarchsJauge.resetColor();
    }

  


}
