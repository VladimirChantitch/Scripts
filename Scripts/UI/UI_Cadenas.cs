using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Cadenas : MonoBehaviour{
    [SerializeField] private AudioClip buttonClip;
    public static UI_Cadenas Instance {get; private set;}
    void Awake(){
        Instance = this;
    }

    [SerializeField] private GameObject digitsCellPrefabs;
    [SerializeField] private Transform digitsParent;
    [SerializeField] private GameObject red_DigitPrefab;
    [SerializeField] private GameObject blue_DigitPrefab;
    [SerializeField] private GameObject green_DigitPrefab;
    [SerializeField] private GameObject yellow_DigitPrefab;
    [SerializeField] private GameObject purple_DigitPrefab;


    [SerializeField] private List<GameObject> cells = new List<GameObject>();
    [SerializeField] private List<UI_CadenasCell> ui_cells = new List<UI_CadenasCell>();

    [SerializeField] private List<int> results = new List<int>(5);
    public void SetResults(int index, int value){
        results[index] = value;
    }
    Cadenas cadenas;
    public void Initialize(Cadenas cadenas){
        this.cadenas = cadenas;

        CleanCells();
        int i = 0;

        foreach(Cadenas.e_digitsColor digitsColor in cadenas.GetDigitsColor()){
            switch (digitsColor){
                case Cadenas.e_digitsColor.Red: digitsCellPrefabs = red_DigitPrefab; break;
                case Cadenas.e_digitsColor.Blue: digitsCellPrefabs = blue_DigitPrefab; break;
                case Cadenas.e_digitsColor.Yellow: digitsCellPrefabs = yellow_DigitPrefab; break;
                case Cadenas.e_digitsColor.Purple: digitsCellPrefabs = purple_DigitPrefab; break;
                case Cadenas.e_digitsColor.Green: digitsCellPrefabs = green_DigitPrefab; break;
            }
                GameObject newCell = Instantiate(digitsCellPrefabs, digitsParent);
                UI_CadenasCell uI_CadenasCell = newCell.GetComponent<UI_CadenasCell>();
                uI_CadenasCell.Initialize(i);
                ui_cells.Add(uI_CadenasCell);
                cells.Add(newCell);

            i += 1;
        }
    }
    private void CleanCells(){
        if (cells.Count >= 1){
            foreach (var cell in cells){
                if (cell != null){
                    Destroy(cell);
                }
            }
        }
    }

    public void ValidateData(){
        List<int> digits = new List<int>();
        for(int i = 0; i < cadenas.GetDigitsColor().Count; i++){
            print(ui_cells[i].GetDigit());
            digits.Add(ui_cells[i].GetDigit());
        }
        cadenas.CheckIfItsGood(results);
    }

    public void EscapeMenu(){
        UI_Manager.Instance.GetLevelCadenas().GetAudioSource().PlayOneShot(buttonClip);
        UI_Manager.Instance.CloseCadenasMenu();
    }
}
