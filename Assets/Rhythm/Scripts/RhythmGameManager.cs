using NUnit.Framework.Internal.Filters;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RhythmGameManager : MonoBehaviour
{

    //please note, i do NOT have any experience creating rhythm games.
    //this is just A way that i came up with implementing it. fully guessing here
    //feel free to change it

    public static RhythmGameManager instance;

    double game_start_time = 0f;
    public double Game_Time;

    bool game_active = true;

    public static UnityEvent<int> end_song;

    [SerializeField] GameObject rhythm_minigame;
    [SerializeField] Indicator left_indicator;
    [SerializeField] Indicator up_indicator;
    [SerializeField] Indicator down_indicator;
    [SerializeField] Indicator right_indicator;

    [SerializeField] Transform map_movement;
    [SerializeField] GameObject[] note_prefab; //left down up right

    [SerializeField] float grace_period;
    [SerializeField] float early_time; //this is to add a miss to prevent the spamming of all notes

    [SerializeField] float bpm = 60f;
    [SerializeField] float note_speed = 1f; //literally multiply everything noterelated by this to maintain the beats

    [SerializeField] TMP_Text miss_text;
    int misses = 0;

    AudioSettings audio_settings;

    int progression = 0; //the current array of the note in the map
    public Note[] Current_Map;

    public SongData current_song_data;


    public enum Direction
    {
        Left, 
        Down,
        Up,
        Right
    }

    double[] QueuedInputs ={ -1, -1, -1, 1}; //left down up right, this will contain -1 if no queued inputs and the time the queued input was hit if there is



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Start_Game(current_song_data);
    }

    void Start_Game(SongData data) //this function will eventually be the function that starts the rhythm section.
    {

        map_movement.position = Vector3.zero;
        instance = this;
        game_start_time = AudioSettings.dspTime;

        rhythm_minigame.SetActive(true);


        bpm = data.bpm;
        note_speed = data.note_speed;

        GenerateStartingRandomSong(data.song_length);

        game_start_time = AudioSettings.dspTime;

    }

    void GenerateStartingRandomSong(int n)
    {

        Note[] notes = new Note[n];

        for (int i = 1; i <= n; i++)
        {

            int new_note_direction = Random.Range(0, 4);

            Note new_note = Instantiate(note_prefab[new_note_direction], new Vector2(((float) new_note_direction * 2) - 3, -i * note_speed * 60f / bpm), Quaternion.identity).GetComponent<Note>();

            new_note.transform.SetParent(map_movement);
            new_note.TimeToHit = i * note_speed * 60f / bpm;

            notes[i - 1] = new_note;

        }

        Current_Map = notes;

    }

    // Update is called once per frame
    void Update()
    {
        if (game_active)
        {
            Game_Time = (AudioSettings.dspTime - game_start_time) * note_speed;

            //manage hit

            //manage queued inputs

            int i = progression;


            if (i >= Current_Map.Length)
            {
                OnSongEnd();
                return;
            }
            float current_input_time = Current_Map[progression].TimeToHit;

            while (Current_Map[i].TimeToHit < Game_Time + grace_period)
            {

                int ok = -1;

                if (!Current_Map[i].hit && Mathf.Abs((float)(QueuedInputs[(int)Current_Map[i].direction] - Current_Map[i].TimeToHit)) <= grace_period) //check if the next note is a hit
                {

                    ok = (int) Current_Map[i].direction;
                    Current_Map[i].Hit();
                    Debug.Log("nice hit");
                    progression = i + 1;

                }

                for (int j = 0; j < 4; j++)
                {
                    if (QueuedInputs[j] != -1 && j != ok)
                    {
                        OnMiss();
                    }
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

            if (progression >= Current_Map.Length)
                return;

            if (Game_Time - Current_Map[progression].TimeToHit > grace_period * note_speed)
            {
                progression++;

                if (!Current_Map[progression].hit)
                {
                    OnMiss();
                }
                
            }

            map_movement.position = new Vector2(map_movement.position.x, (float) Game_Time);
        }
    }

    void OnMiss()
    {
        misses++;

        miss_text.text = "Misses: " + misses;
        Debug.Log("you missed!");
        //
    }

    void OnSongEnd()
    {
        end_song.Invoke(misses);

        rhythm_minigame.SetActive(false);

        Debug.Log("song over");
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
