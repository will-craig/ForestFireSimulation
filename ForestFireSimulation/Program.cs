using System.Text;

namespace ForestFireSimulation;

static class Program
{
    enum CellState
    {
        Earth,
        Tree,
        Burning
    }
    public static void Main(string[] args)
    {
        var grid = new List<List<CellState>>();
        InitializeGrid(grid, 10, 10);
        PrintGrid(grid);
        Console.WriteLine("Starting simulation...");
        var newGrid = MutateGrid(grid);
        PrintGrid(newGrid);
    }

    private static List<List<CellState>> MutateGrid(List<List<CellState>> grid)
    {
        var newGrid = new List<List<CellState>>();
        foreach (var row in grid)
        {
            var newRow = new List<CellState>();
            foreach (var cell in row)
            {
                newRow.Add(TransformCell(cell));
            }
            newGrid.Add(newRow);
        }
        return newGrid;
    }

    private static void PrintGrid(List<List<CellState>> grid)
    {
        StringBuilder sb = new StringBuilder();
        Console.OutputEncoding = System.Text.Encoding.UTF8; 
        foreach (var row in grid)
        {
            foreach (var cell in row)
            {
                sb.Append("|");
                sb.Append(cell switch
                {
                    CellState.Earth => "\ud83d\udfeb", // 🟫
                    CellState.Tree => "\ud83c\udf32", //🌲
                    CellState.Burning => "\ud83d\udd25", // 🔥
                    _ => "?"
                });
            }
            sb.Append("|");
            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }

    static void InitializeGrid(List<List<CellState>> grid, int rows, int cols)
    {
        var random = new Random();
        for (int i = 0; i < rows; i++)
        {
            var row = new List<CellState>();
            for (int j = 0; j < cols; j++)
            {
                row.Add(random.Next(0, 3) switch
                {
                    0 => CellState.Earth,
                    1 => CellState.Tree,
                    _ => CellState.Burning
                });
            }
            grid.Add(row);
        }
    }
    
    static CellState TransformCell(CellState cell)
    {
        return cell switch
        {
            CellState.Earth => CellState.Tree,
            CellState.Tree => CellState.Burning,
            CellState.Burning => CellState.Earth,
            _ => cell
        };
    }
}