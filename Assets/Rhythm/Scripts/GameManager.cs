using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //please note, i do NOT have any experience creating rhythm games.
    //this is just A way that i came up with implementing it.
    //feel free to change it

    public static GameManager instance;

    float game_start_time = 0f;
    public float Game_Time;

    bool game_active = true;

    [SerializeField] Indicator left_indicator;
    [SerializeField] Indicator up_indicator;
    [SerializeField] Indicator down_indicator;
    [SerializeField] Indicator right_indicator;

    [SerializeField] float grace_period;


    int progression = 0; //the current array of the note in the map
    public Note[] Current_Map;


    public enum Direction
    {
        Left, 
        Down,
        Up,
        Right
    }

    float[] QueuedInputs ={ -1, -1, -1, 1}; //left down up right, this will contain -1 if no queued inputs and the time the queued input was hit if there is



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        game_start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (game_active)
        {
            Game_Time = Time.time - game_start_time;

            //manage hit


            //manage queued inputs
            int i = progression;
            float current_input_time = Current_Map[progression].TimeToHit;
            while (Current_Map[i].TimeToHit < current_input_time + grace_period)
            {

                if (Mathf.Abs(QueuedInputs[(int)Current_Map[i].direction] - Current_Map[i].TimeToHit) < grace_period) //check if the next note is a hit
                {
                    Current_Map[i].Hit();
                    progression = i;
                }

                i++;

                if (i >= Current_Map.Length)
                    break;
            }

            for (i = 0; i < 4; i++)
            {
                QueuedInputs[i] = -1;
            }
            


            if (Game_Time - Current_Map[progression].TimeToHit > grace_period && !Current_Map[progression].hit)
            {
                progression++;
                Debug.Log("you missed, dumbass");

                //miss code here
            }
        }
    }

    void Start_Game() //this function will eventually be the function that starts the rhythm game.
    {

        game_start_time = Time.time;
    }

    private void OnLeft()
    {
        left_indicator.Indicate();
        QueuedInputs[0] = Game_Time;
    }

    private void OnDown()
    {
        down_indicator.Indicate();
        QueuedInputs[1] = Game_Time;
    }

    private void OnUp()
    {
        up_indicator.Indicate();
        QueuedInputs[2] = Game_Time;
    }

    

    

    private void OnRight()
    {
        right_indicator.Indicate();
        QueuedInputs[3] = Game_Time;
    }

}
