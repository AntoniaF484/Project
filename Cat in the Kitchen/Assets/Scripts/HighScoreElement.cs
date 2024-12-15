using System;

[Serializable]
public class HighScoreElement
{
    public string playerName;

    public int points;

    public HighScoreElement(string name, int points)
    {
        playerName = name;
        this.points = points;
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
