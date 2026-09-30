namespace ConnectFour;

public class GameState
{
  public enum WinState
  {
    NoWinner,
    Player1_Wins,
    Player2_Wins,
    Tie
  }

  public int[] Board { get; private set; } = new int[42];
  public int PlayerTurn { get; private set; } = 1;
  public WinState Winner { get; private set; } = WinState.NoWinner;
  public List<string> MoveHistory { get; private set; } = new();

  public int Player1Wins { get; set; } = 0;
  public int Player2Wins { get; set; } = 0;

  public void ResetBoard()
  {
    Board = new int[42];
    PlayerTurn = 1;
    Winner = WinState.NoWinner;
    MoveHistory.Clear();
  }

  public void ResetScore()
  {
    Player1Wins = 0;
    Player2Wins = 0;
    ResetBoard();
  }

  public int PlayPiece(int col)
  {
    if (Winner != WinState.NoWinner) return -1;

    for (int row = 5; row >= 0; row--)
    {
      int index = row * 7 + col;
      if (Board[index] == 0)
      {
        Board[index] = PlayerTurn;
        MoveHistory.Add($"Player {PlayerTurn} placed piece at Column {col + 1}, Row {row + 1}");

        CheckForWin();

        if (Winner == WinState.NoWinner)
        {
          PlayerTurn = PlayerTurn == 1 ? 2 : 1;
        }
        else if (Winner == WinState.Player1_Wins)
        {
          Player1Wins++;
        }
        else if (Winner == WinState.Player2_Wins)
        {
          Player2Wins++;
        }

        return index;
      }
    }

    return -1;
  }

  private void CheckForWin()
  {
    int[] winningPatterns = new[]
    {
            0,1,2,3, 1,2,3,4, 2,3,4,5, 3,4,5,6,
            7,8,9,10, 8,9,10,11, 9,10,11,12, 10,11,12,13,
            14,15,16,17, 15,16,17,18, 16,17,18,19, 17,18,19,20,
            21,22,23,24, 22,23,24,25, 23,24,25,26, 24,25,26,27,
            28,29,30,31, 29,30,31,32, 30,31,32,33, 31,32,33,34,
            35,36,37,38, 36,37,38,39, 37,38,39,40, 38,39,40,41,

            0,7,14,21, 7,14,21,28, 14,21,28,35,
            1,8,15,22, 8,15,22,29, 15,22,29,36,
            2,9,16,23, 9,16,23,30, 16,23,30,37,
            3,10,17,24, 10,17,24,31, 17,24,31,38,
            4,11,18,25, 11,18,25,32, 18,25,32,39,
            5,12,19,26, 12,19,26,33, 19,26,33,40,
            6,13,20,27, 13,20,27,34, 20,26,32,38,

            14,22,30,38, 7,15,23,31, 15,23,31,39, 0,8,16,24, 8,16,24,32, 16,24,32,40,
            1,9,17,25, 9,17,25,33, 17,25,33,41, 2,10,18,26, 10,18,26,34, 3,11,19,27,

            3,9,15,21, 4,10,16,22, 10,16,22,28, 5,11,17,23, 11,17,23,29, 17,23,29,35,
            6,12,18,24, 12,18,24,30, 18,24,30,36, 13,19,25,31, 19,25,31,37, 20,26,32,38
        };

    for (int i = 0; i < winningPatterns.Length; i += 4)
    {
      int p1 = winningPatterns[i];
      int p2 = winningPatterns[i + 1];
      int p3 = winningPatterns[i + 2];
      int p4 = winningPatterns[i + 3];

      if (Board[p1] != 0 &&
          Board[p1] == Board[p2] &&
          Board[p1] == Board[p3] &&
          Board[p1] == Board[p4])
      {
        Winner = Board[p1] == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
        return;
      }
    }

    if (!Board.Contains(0))
    {
      Winner = WinState.Tie;
    }
  }
}