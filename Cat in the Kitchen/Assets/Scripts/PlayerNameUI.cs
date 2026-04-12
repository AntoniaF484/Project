
using TMPro;
using Unity.Netcode;


public class PlayerNameUI : NetworkBehaviour
{

    public TextMeshProUGUI playerNameText;
    public IndividualPlayerStats playerStats;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = GetComponentInParent<IndividualPlayerStats>();
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNameTag();
    }

    void UpdateNameTag()
    {
        
        if (playerStats == null) return;
       playerNameText.text = playerStats.playerName.Value.ToString();

    }
}
