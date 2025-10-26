using NUnit.Framework.Internal.Filters;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RhythmGameManager : MonoBehaviour
{

    //please note, i do NOT have any experience creating rhythm games.
    //this is just A way that i came up with implementing it.
    //feel free to change it

    public static RhythmGameManager instance;

    float game_start_time = 0f;
    public float Game_Time;

    bool game_active = true;

    [SerializeField] Indicator left_indicator;
    [SerializeField] Indicator up_indicator;
    [SerializeField] Indicator down_indicator;
    [SerializeField] Indicator right_indicator;

    [SerializeField] Transform map_movement;
    [SerializeField] GameObject note_prefab;

    [SerializeField] float grace_period;

    [SerializeField] float bpm;
    float speed_distance_modifier;

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

        speed_distance_modifier = bpm / 60;


        GenerateStartingRandomSong(10);

    }

    void GenerateStartingRandomSong(int n)
    {

        Note[] notes = new Note[(int) (n * 2)];

        for (int i = 1; i <= n * 2; i++)
        {

            Direction new_note_direction = (Direction) Random.Range(0, 4);

            Note new_note = Instantiate(note_prefab, new Vector2(((float)((float) new_note_direction) * 2) - 3, -((int)(i / 2))), Quaternion.identity).GetComponent<Note>();
            
            switch ((int)new_note_direction) {

                case 0:

                    new_note.transform.Rotate(new Vector3(0, 0, 90));
                    new_note.direction = Direction.Left;
                    
                    break;

                case 1:

                    new_note.transform.Rotate(new Vector3(0, 0, 180));
                    new_note.direction = Direction.Down;

                    break;

                case 2:

                    new_note.transform.Rotate(new Vector3(0, 0, 0));
                    new_note.direction = Direction.Up;

                    break;

                case 3:

                    new_note.transform.Rotate(new Vector3(0, 0, -90));
                    new_note.direction = Direction.Right;

                    break;
            }

            new_note.transform.SetParent(map_movement);
            new_note.TimeToHit = (int) (i / 2);

            notes[i - 1] = new_note;

        }

        Current_Map = notes;

    }

    // Update is called once per frame
    void Update()
    {
        if (game_active)
        {
            Game_Time = (Time.time - game_start_time) * speed_distance_modifier;

            //manage hit

            //manage queued inputs

            int i = progression;
            float current_input_time = Current_Map[progression].TimeToHit;
            while (Current_Map[i].TimeToHit < Game_Time + grace_period)
            {

                if (!Current_Map[i].hit && Mathf.Abs(QueuedInputs[(int)Current_Map[i].direction] - Current_Map[i].TimeToHit) < grace_period) //check if the next note is a hit
                {
                    Current_Map[i].Hit();
                    progression = i + 1;
                }

                i++;

                if (i >= Current_Map.Length)
                    break;
            }

            //reset inputs

            for (i = 0; i < 4; i++)
            {
                QueuedInputs[i] = -1;
            }
            

            //check if the current note is outside the ability to actually hit within the grace period-- if it is, the note is missed

            if (Game_Time - Current_Map[progression].TimeToHit > grace_period)
            {
                progression++;

                if (!Current_Map[progression].hit)
                    Debug.Log("you missed");

                    //miss code here
            }

            map_movement.position = new Vector2(map_movement.position.x, Game_Time);
        }
    }

    void Start_Game() //this function will eventually be the function that starts the rhythm section.
    {
        game_start_time = Time.time;
    }

    //inputs below

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
