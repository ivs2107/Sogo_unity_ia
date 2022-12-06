using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    /* public string[,,] tabMap = new string[4,4,4] 
     {
             {{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"}},
             {{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"}},
             {{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"}},
             {{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"},{"0","0","0","0"}},
     };*/
    Map map = new Map(4, 4, 4);

    private const int PointToWin = 4;

    int test = 0;

    //compatge des point streak + floor
    public bool isPlayerTurn = true;

    public InterfaceManager InterfaceManager;


    private void Start()
    {
      //  Map map = new Map(4, 4, 4);
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        InterfaceManager.ChangeInterface(BoolToString());
    }

    public void SetBallInArray(int row, int column, int floor)
    {
        map.tabMap[floor, column, row] = BoolToString();
        if (checkGameWin(map, row, column, floor, BoolToString()) <=0)
        {
            InterfaceManager.InterfaceWin(BoolToString());
        }
       


        if (BoolToString() == "Blue")
        {
            map.tabMap[floor, column, row] = BoolToString();

            if (CheckDraw(map.tabMap))
            {
                InterfaceManager.InterfaceDraw();
            }
            //  Invoke("IAPlay", 1);
            ChangeTurn();
            int x, y, z;
            int result = minimax(map, "Red", out x, out y, out z, 0, row, column, floor,true,int.MinValue, int.MaxValue);

            if (x != -1)
            {
                map.tabMap[z, y, x] = "Red";
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Zone"))
                {
                    if (g.GetComponent<SelectionRod>().Column == y && g.GetComponent<SelectionRod>().Row == x)
                    {
                        g.GetComponent<SelectionRod>().InstantiateBall();
                       // g.GetComponent<SelectionRod>().IAPlay();
                    }
                }
            }
        }
        if(BoolToString() == "Red")
        {
            if (CheckDraw(map.tabMap))
            {
                InterfaceManager.InterfaceDraw();
            }
            ChangeTurn();
           // map.tabMap[z, y, x] = "Blue"
        }
    }


    private string BoolToString()
    {
        if (isPlayerTurn == true) return "Blue";
        else return "Red";
    }
    //supérieur professionnel parlé de xxxy
    private int checkGameWin(Map Grid,int row, int column, int floor, string Player)
    {
        List<int> listScoreComputer = new List<int>();
        listScoreComputer.Add(CheckDirection(row, column, floor, 1, 0, 0, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 0, 1, 0, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 0, 0, 1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 1, 1, 0, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 1, 1, 1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 1, 1, -1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 1, 0, 1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, -1, 0, 1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 0, 1, 1, Grid.tabMap.GetLength(2), Player));
        listScoreComputer.Add(CheckDirection(row, column, floor, 0, -1, 1, Grid.tabMap.GetLength(2), Player));


        // listScore.Sort();
        var ScoreComputer = listScoreComputer.Sum();


        List<int> listScorePlayer = new List<int>();
        listScorePlayer.Add(CheckDirection(row, column, floor, 1, 0, 0, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 0, 1, 0, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 0, 0, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 1, 1, 0, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 1, 1, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 1, 1, -1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 1, 0, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, -1, 0, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 0, 1, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));
        listScorePlayer.Add(CheckDirection(row, column, floor, 0, -1, 1, Grid.tabMap.GetLength(2), switchPiece(Player)));


        // listScore.Sort();
        var ScorePlayer =  listScorePlayer.Sum();

        return (ScoreComputer - ScorePlayer);



        /*
        if (CheckDirection(row, column, floor, 1,0,0, Grid.tabMap.GetLength(2), Player) //contrôle row
            || CheckDirection(row, column, floor, 0,1,0, Grid.tabMap.GetLength(1), Player) //contrôle colonne
            || CheckDirection(row, column, floor, 0,0,1, Grid.tabMap.GetLength(0), Player)  //contrôle floor
            || CheckDirection(row, column, floor, 1, 1, 0, Grid.tabMap.GetLength(0), Player)  //contrôle diagonale plat ligne
            || CheckDirection(row, column, floor, 1, 1, 1, Grid.tabMap.GetLength(0), Player)  //contrôle diagonale avec etage ligne de gauche à droite
            || CheckDirection(row, column, floor, 1, 1, -1, Grid.tabMap.GetLength(0), Player)  //contrôle diagonale avec etage ligne de droite gauche
            || CheckDirection(row, column, floor, 1, -1, 0, Grid.tabMap.GetLength(0), Player) //contrôle diagonale plat ligne mais l'autre
            || CheckDirection(row, column, floor, 1, 0, 1, Grid.tabMap.GetLength(0), Player)//contrôle diagonale colonne gauche droite
            || CheckDirection(row, column, floor, -1, 0, 1, Grid.tabMap.GetLength(0), Player)//contrôle diagonale colonne gauche droite
            || CheckDirection(row, column, floor, 0, 1, 1, Grid.tabMap.GetLength(0), Player)//contrôle diagonale colonne derrière devant
            || CheckDirection(row, column, floor, 0, -1, 1, Grid.tabMap.GetLength(0), Player)//contrôle diagonale colonne devant derrière
            )
        {
         //   Debug.Log("win du player : " + BoolToString());
          //  InterfaceManager.InterfaceWin(BoolToString());
            return true;
        }
        return false;*/
    }

    //renvoyer streak pour le comptage de point
    //font code
    //github
    //couleur pour variable

    //je remarque quil passe trop de fois genre il se compte lui meme(celui qui est posé) 2 fois 
    /// <summary>
    /// Reagrde si un joueur à 4 billes à la suite dépendant d'un direction
    /// </summary>
    /// <param name="row">Position dans la ligne de la bille dernière bille posée</param>
    /// <param name="column">Position dans la colonne de la bille dernière bille posée</param>
    /// <param name="floor">Position dans l'étage de la bille dernière bille posée</param>
    /// <param name="MultiplicateurRow">Multiplicateur dans la ligne</param>
    /// <param name="MultiplicateurColumn">Multiplicateur dans la colonne</param>
    /// <param name="MultiplicateurFloor">Multiplicateur dans l'étage</param>
    /// <param name="lenght">longueur du tableau</param>
    /// <returns></returns>
    private int CheckDirection(int row, int column, int floor, int MultiplicateurRow, int MultiplicateurColumn, int MultiplicateurFloor, int lenght, string Player)
    {
        //Création d'uu tableau associatif
        IDictionary<string, int> listDirectionMultiplicator = new Dictionary<string, int>();
        listDirectionMultiplicator["row"] = MultiplicateurRow;
        listDirectionMultiplicator["column"] = MultiplicateurColumn;
        listDirectionMultiplicator["floor"] = MultiplicateurFloor;


        //série de bille cumulé
        int streak = 1;
        int Score = 0;
        for (int i = 0; i < 2; i++)
        {
            //boucle parcourant une direction d'un côté
            for (int forward = 1; forward < PointToWin; forward++)
            {
                //test pour voir si il sort du tableau
                if (
                    lenght - 1 < row + (forward * listDirectionMultiplicator["row"]) && listDirectionMultiplicator["row"] == 1
                    || lenght - 1 < column + (forward * listDirectionMultiplicator["column"]) && listDirectionMultiplicator["column"] == 1
                    || lenght - 1 < floor + (forward * listDirectionMultiplicator["floor"]) && listDirectionMultiplicator["floor"] == 1
                    || 0 > row + (forward * listDirectionMultiplicator["row"]) && listDirectionMultiplicator["row"] == -1
                    || 0 > column + (forward * listDirectionMultiplicator["column"]) && listDirectionMultiplicator["column"] == -1
                    || 0 > floor + (forward * listDirectionMultiplicator["floor"]) && listDirectionMultiplicator["floor"] == -1
                    )
                {
                    break;
                }
                else
                {
                    //test pour vour voir si il y a une bille semblable à celle posée
                    if (map.tabMap[
                        floor + (forward * listDirectionMultiplicator["floor"]),
                        column + (forward * listDirectionMultiplicator["column"]),
                        row + (forward * listDirectionMultiplicator["row"])
                        ]
                        == Player)
                    {
                        streak++;

                    }
                    else if(map.tabMap[
                        floor + (forward * listDirectionMultiplicator["floor"]),
                        column + (forward * listDirectionMultiplicator["column"]),
                        row + (forward * listDirectionMultiplicator["row"])
                        ]
                        == switchPiece(Player))
                    {
                        Score = 0;
                        return Score;
                    }
                }
            }
            //inversion des valeurs du tableau multiplicateur
            IDictionary<string, int> listDirectionMultiplicatorCopy = new Dictionary<string, int>();
            foreach (var Multi in listDirectionMultiplicator)
            {
                listDirectionMultiplicatorCopy[Multi.Key]  = Multi.Value * -1;
            }
            foreach(var MultiCopy in listDirectionMultiplicatorCopy)
            {
                listDirectionMultiplicator[MultiCopy.Key] = MultiCopy.Value;
            }
        }

        //test pour voir si il y a assez de bille qui se suivent pour gagner
     /*   if (streak == PointToWin)
        {
            return true;
        }*/
        if(streak == 1)
        {
            Score = 10;
        }
        if (streak == 2)
        {
            Score = 100;
        }
        if (streak == 3)
        {
            Score = 1000;
        }
        if (streak == 4)
        {
            Score = 100000;
        }
        //return false;
        return Score;
    }


    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool CheckDraw(string[,,] tab)
    {
        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(0); j++)
            {
                for (int k = 0; k < tab.GetLength(2); k++)
                {
                    if(tab[i,j,k] != ".")
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }


    //relation avec le minimax


    private int minimax(Map InputGrid, string Player, out int x, out int y, out int z, int dump, int rowCheck, int columnCheck, int floorCheck,bool maximizingPlayer, int alpha, int beta)
    {
        /*if(dump == 2)
        {
            z = rowCheck;
            y = columnCheck;
            x = floorCheck;
            return 0;
        }*/
        bool CanBreak = false;
        Map Grid = cloneGrid(InputGrid);
        int MaxScoreIndex = int.MinValue;
        int MinScoreIndex = int.MaxValue;
        x = y = z = -1;
        if (dump == 3)
        {
            return 0;
        }
        if (checkScore(Grid, rowCheck,columnCheck,floorCheck, dump) != 0)
            return checkScore(Grid, rowCheck, columnCheck, floorCheck, dump);
        else if (CheckDraw(Grid.tabMap)) return 0;
        //  switchPiece(Player);

      //  if (maximizingPlayer)
     //   {
            int best = int.MinValue;
            for (int floor = 0; floor < 4; floor++)
            {
                for (int col = 0; col < 4; col++)
                {
                    for (int row = 0; row < 4; row++)
                    {
                        if (Grid.tabMap[floor, col, row] == ".")
                        {
                            int childX, childY, childZ;
                            rowCheck = row;
                            columnCheck = col;
                            floorCheck = floor;
                            int value = minimax(makeGridMove(Grid, Player, floor, col, row), switchPiece(Player), out childX, out childY, out childZ, dump + 1, rowCheck, columnCheck, floorCheck, false, alpha, beta);
                            best = Math.Max(best, value);
                            alpha = Math.Max(alpha, value);
                            if (Player == "Red")
                            {
                                if (value > MaxScoreIndex)
                                {
                                    MaxScoreIndex = value;
                                    x = row;
                                    y = col;
                                    z = floor;
                                 //   Debug.Log("*");
                                //Console.WriteLine("*");
                                }
                            }
                            else
                            {
                                if (value < MinScoreIndex)
                                {
                                    MinScoreIndex = value;
                                    x = row;
                                    y = col;
                                    z = floor;
                                  //  Debug.Log("*");
                                //Console.WriteLine("*");
                                }
                            }
                         /*   if (best <= alpha)
                            {
                                CanBreak = true;
                                break;
                            }*/




                            //scores.Add(minimax(makeGridMove(Grid, Player, i,j), switchPiece(Player)));
                            //moves.Add(new int[]{j,i});
                        }
                    }
                 /*   if (CanBreak)
                    {
                        break;
                    }*/
                }
              /*  if (CanBreak)
                {
                    break;
                }*/
            }
        /*  }
          else
          {
              int best = int.MaxValue;
              for (int floor = 0; floor < 4; floor++)
              {
                  for (int col = 0; col < 4; col++)
                  {
                      for (int row = 0; row < 4; row++)
                      {
                          if (Grid.tabMap[floor, col, row] == ".")
                          {
                              int childX, childY, childZ;
                              rowCheck = row;
                              columnCheck = col;
                              floorCheck = floor;
                              int value = minimax(makeGridMove(Grid, Player, floor, col, row), switchPiece(Player), out childX, out childY, out childZ, dump + 1, rowCheck, columnCheck, floorCheck, true, alpha, beta);
                              best = Math.Min(best, value);
                              alpha = Math.Min(alpha, value);

                              if (Player == "Red")
                              {
                                  if (best > MaxScoreIndex)
                                  {
                                      MaxScoreIndex = best;
                                      x = row;
                                      y = col;
                                      z = floor;
                                      //  Console.WriteLine("*");
                                  }
                              }
                              else
                              {
                                  if (best < MinScoreIndex)
                                  {
                                      MinScoreIndex = best;
                                      x = row;
                                      y = col;
                                      z = floor;
                                      //   Console.WriteLine("*");
                                  }
                              }
                              if (best <= alpha)
                              {
                                  CanBreak = true;
                                  break;
                              }



                              //scores.Add(minimax(makeGridMove(Grid, Player, i,j), switchPiece(Player)));
                              //moves.Add(new int[]{j,i});
                          }
                      }
                      if (CanBreak)
                      {
                          break;
                      }
                  }
                  if (CanBreak)
                  {
                      break;
                  }
              }
          }*/
       // Debug.Log("*");
       //Console.WriteLine("*");
        if (Player == "Red")
        {
            //int MaxScoreIndex = scores.IndexOf(scores.Max());
            //Choice = moves[MaxScoreIndex];
            //return scores.Max();
            return MaxScoreIndex;
        }
        else
        {
            //int MinScoreIndex = scores.IndexOf(scores.Min());
            //Choice = moves[MinScoreIndex];
            //return scores.Min();
            return MinScoreIndex;
        }
    }



    static Map cloneGrid(Map Grid)
    {
        Map Clone = new Map(4,4,4);
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                    Clone.tabMap[i,j,k] = Grid.tabMap[i,j,k];

        return Clone;
    }


    private int checkScore(Map Grid,int x, int y, int z, int dump)
    {
        /* if (checkGameWin(Grid, x, y, z, "Red"))
         {
             Debug.Log("win du player : Red");
             return 10 - dump;
         }

         else if (checkGameWin(Grid, x, y, z, "Blue"))
             return -10 + dump;

         else return 0;*/
        return checkGameWin(Grid, x, y, z, "Red");
    }


    static Map makeGridMove(Map Grid, string Move, int floor, int col, int row)
    {
        Map newGrid = cloneGrid(Grid);
        newGrid.tabMap[floor, col,row] = Move;
        return newGrid;
    }

    private string switchPiece(string Piece)
    {
        if (Piece == "Blue") return "Red";
        else return "Blue";
        //return PlayerTurn();
    }























































































    private bool CheckDirectionOLD(int row, int column, int floor, string direction, int lenght)
    {
        IDictionary<string, int> listDirection = new Dictionary<string, int>();
        listDirection["row"] = row;
        listDirection["column"] = column;
        listDirection["floor"] = floor;

        IDictionary<string, int> listDirectionMultiplicator = new Dictionary<string, int>();
        listDirectionMultiplicator["row"] = 0;
        listDirectionMultiplicator["column"] = 0;
        listDirectionMultiplicator["floor"] = 0;
        //je remarque quil passe trop de fois genre il se compte lui meme(celui qui est posé) 2 fois 

        listDirectionMultiplicator[direction] = 1;

        int streak = 1;
        for (int forward = 1; forward < PointToWin; forward++)
        {
            if (lenght - 1 < listDirection[direction] + forward)
            {
                break;
            }
            else
            {
                if (map.tabMap[floor + (forward * listDirectionMultiplicator["floor"]), column + (forward * listDirectionMultiplicator["column"]), row + (forward * listDirectionMultiplicator["row"])] == BoolToString())
                {
                    streak++;
                }
            }
        }

        for (int backward = 1; backward < lenght; backward++)
        {
            if (0 > listDirection[direction] - backward)
            {
                break;
            }
            else
            {
                if (map.tabMap[floor - (backward * listDirectionMultiplicator["floor"]), column - (backward * listDirectionMultiplicator["column"]), row - (backward * listDirectionMultiplicator["row"])] == BoolToString())
                {
                    streak++;
                }
            }
        }

        if (streak == PointToWin)
        {
            return true;
        }
        return false;
    }









}
