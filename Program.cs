using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace bt2
{

    class Program
    {
//        int[,] ships = new int[10, 10]
//            {
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0},
//            {0,0,0,0,0,0,0,0,0,0}
//};
        public static int[,] Myships = new int[,]{
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}};

        // 0 - empty cell
        // 1 - for MyShipss - cell contains ship
        // 1 - for EnemyShips - cell contains ship(and u shooted it )
        // 9 - for MyShips - cell where u cant place Ship
        // 9 - for EnemyShips - cell where u cant shoot(no ships there)
        public static int[,] Enemyships = new int[,]{
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}};
        public static byte WhoWin = 0;

        public static void SetRestrictedCells(List<int[]> tmp)
        {
            List<int[]> restrictedCells = new List<int[]>();
            for (int i = 0; i < tmp.Count; i++)
            {
                if (restrictedCells.Contains(tmp[i]))
                {
                    restrictedCells.Remove(tmp[i]);
                }
                if (tmp[i][0] == 0)
                {
                    if (tmp[i][1] == 0)
                    {
                        //                        restrictedCells.Add(new int[] { tmp[i][0] - 1,tmp[i][1] });
                        //                      restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1]9 });
                        //                    restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1]+1 });

                        //                  restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        //                restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] 9});
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });
                    }
                    else if (tmp[i][1] == 9)
                    {
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        //                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        //                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                    else
                    {
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                }
                else if (tmp[i][0] == 9)
                {
                    if (tmp[i][1] == 0)
                    {
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        //restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                    else if (tmp[i][1] == 9)
                    {
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                    else
                    {
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                }
                else
                {
                    if (tmp[i][1]==0)
                    {

                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        //restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        //restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });
                    }
                    else if (tmp[i][1]==9)
                    {

                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
//                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
  //                      restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
    //                    restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                    else
                    {

                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] - 1, tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0], tmp[i][1] + 1 });

                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] - 1 });
                        restrictedCells.Add(new int[] { tmp[i][0] + 1, tmp[i][1] + 1 });

                    }
                }

            }
            for (int i = 0; i < restrictedCells.Count; i++)
            {
                if (Myships[restrictedCells[i][0], restrictedCells[i][1]] ==0)
                {
                    Myships[restrictedCells[i][0], restrictedCells[i][1]] = 9;
                }
            }
        }

        public static void SetFourCellShip()
        {
            Console.WriteLine("Set 4 cell batlleship cell by cell");
            int[] cells = new int[8]; //max cells 4 for x point, 4 for y point
            Console.WriteLine("Enter X coordinate coordinate");
            string line = Console.ReadLine();
            int value;
            if (int.TryParse(line, out value))
            {
                cells[0] = value;
            }
            else
            {
                throw new Exception("Not an integer!");
            }

            Console.WriteLine("Enter Y coordinate coordinate");
            line = Console.ReadLine();
            if (int.TryParse(line, out value))
            {
                cells[1] = value;
            }
            else
            {
                throw new Exception("Not an integer!");
            }
            // 1 cell in 2-deimensional is determined
            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("Enter X coordinate");
                line = Console.ReadLine();
                if (int.TryParse(line, out value))
                {
                    cells[2*i] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }

                Console.WriteLine("Enter Y coordinate");
                line = Console.ReadLine();
                if (int.TryParse(line, out value))
                {
                    cells[2*i+1] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }
                if (cells[2*i]!=cells[0] && cells[2*i+1]!=cells[1])// && (cells[2*i]!=cells[i]-1 && cells[2*i+1]!=cells[i+1]) && (cells[2 * i] != cells[i] +1 && cells[2 * i + 1] != cells[i + 1]) && (cells[2 * i] != cells[i] && cells[2 * i + 1] != cells[i + 1]-1)&& (cells[2 * i] != cells[i]  && cells[2 * i + 1] != cells[i + 1]+1)
                {
                    throw new Exception("One ship per 1 line");
                }

            }//determining 3 more cells with checking if inline

            List<int[]> tmp = new List<int[]>();
            for (int i = 0; i < 4; i++)//placing determined cells(4 cells - 1 ship)
            {
                Myships[cells[2 * i], cells[2 * i + 1]] = 4;
                tmp.Add(new int[] { cells[2 * i], cells[2 * i + 1] });
            }
            SetRestrictedCells(tmp);
            DrawBoard();
        }

        public static void SetThreeCellShips()
        {
            
            for (int i = 0; i < 2; i++) //we got 2 3-cell ships
            {
                Console.WriteLine("Set 3 cell battleship");
                int[] cells = new int[6]; //for 1 ship we need 6 int's
                Console.WriteLine("Enter X coordinate");
                string line = Console.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    cells[0] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }

                Console.WriteLine("Enter Y coordinate");
                line = Console.ReadLine();
                if (int.TryParse(line, out value))
                {
                    cells[1] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }
                //we determined 1 cell
                for (int j = 1; j < 3; j++)
                {
                    Console.WriteLine("Enter X coordinate");
                    line = Console.ReadLine();
                    if (int.TryParse(line, out value))
                    {
                        cells[2 * j] = value;
                    }
                    else
                    {
                        throw new Exception("Not an integer!");
                    }

                    Console.WriteLine("Enter Y coordinate");
                    line = Console.ReadLine();
                    if (int.TryParse(line, out value))
                    {
                        cells[2 * j + 1] = value;
                    }
                    else
                    {
                        throw new Exception("Not an integer!");
                    }
                    if (cells[2 * j] != cells[0] && cells[2 * j + 1] != cells[1] )
                    {
                        throw new Exception("One ship on 1 line");
                    }

                }//determining 2 more cells wiht checking if inline
                List<int[]> tmp = new List<int[]>();
                for (int j = 0; j < 3; j++)//placing determined cells
                {
                    if (Myships[cells[2 * j], cells[2 * j + 1]] == 0)
                    {
                        Myships[cells[2 * j], cells[2 * j + 1]] = 3;
                        tmp.Add(new int[] { cells[2 * j], cells[2 * j + 1] });
                    }
                    else throw new Exception("U cant place ship there");
                    
                }
                SetRestrictedCells(tmp);//set restricted cells
                DrawBoard();
            }
           
        }

        public static void SetTwoCellShips()
        {
            for (int i = 0; i < 3; i++)//we got 3 2-cell ships
            {
                Console.WriteLine("Set 2 cell battleship");
                int[] cells = new int[4];//need 4 int's for 1 ship
                //determinig first cell
                Console.WriteLine("Enter X coordinate");
                string line = Console.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    cells[0] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }

                Console.WriteLine("Enter Y coordinate");
                line = Console.ReadLine();
                if (int.TryParse(line, out value))
                {
                    cells[1] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }
                for (int j = 1; j < 2; j++)//determining second cell
                {
                    Console.WriteLine("Enter X coordinate");
                    line = Console.ReadLine();
                    if (int.TryParse(line, out value))
                    {
                        cells[2 * j] = value;
                    }
                    else
                    {
                        throw new Exception("Not an integer!");
                    }

                    Console.WriteLine("Enter Y coordinate");
                    line = Console.ReadLine();
                    if (int.TryParse(line, out value))
                    {
                        cells[2 * j + 1] = value;
                    }
                    else
                    {
                        throw new Exception("Not an integer!");
                    }
                    if (cells[2 * j] != cells[0] && cells[2 * j + 1] != cells[1])//check if inline (first 2) last 2 checking if cells are near 
                    {
                        throw new Exception("One ship on 1 line");
                    }

                }
                List<int[]> tmp = new List<int[]>();
                for (int j = 0; j < 2; j++)//placing determinied  cells
                {
                    if (Myships[cells[2 * j], cells[2 * j + 1]] == 0)
                    {
                        Myships[cells[2 * j], cells[2 * j + 1]] = 2;
                        tmp.Add(new int[] { cells[2 * j], cells[2 * j + 1] });
                    }
                    else throw new Exception("U cant place ship there");

                }
                SetRestrictedCells(tmp);
                DrawBoard();
            }
        }

        public static void SetOneCellShips()
        {
            for (int i = 0; i < 4; i++)//we got 4 2-cell ships
            {
                Console.WriteLine("Set 1 cell battleship");
                int[] cells = new int[2];//need 2 int's for 1 ship
                //determinig first cell
                Console.WriteLine("Enter X coordinate");
                string line = Console.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    cells[0] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }

                Console.WriteLine("Enter Y coordinate");
                line = Console.ReadLine();
                if (int.TryParse(line, out value))
                {
                    cells[1] = value;
                }
                else
                {
                    throw new Exception("Not an integer!");
                }
              
                List<int[]> tmp = new List<int[]>();
                for (int j = 0; j < 1; j++)//placing determinied  cells
                {
                    if (Myships[cells[2 * j], cells[2 * j + 1]] == 0)
                    {
                        Myships[cells[2 * j], cells[2 * j + 1]] = 1;
                        tmp.Add(new int[] { cells[2 * j], cells[2 * j + 1] });
                    }
                    else throw new Exception("U cant place ship there");
                }
                SetRestrictedCells(tmp);
                DrawBoard();
            }
        }

        public static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  |A B C D E F G H I J");
            Console.WriteLine("  (0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("----------------------");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + " |");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(Myships[i,j] + " " );
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static bool GetYourResult(string result, int[] cells)
        {
            int val = Int16.Parse(result);
            Myships[cells[0], cells[1]] = val;
            if (val>0)
            {
                return true;
            }
            return false;
        }

        public static void CheckTablesIfWin()
        { }

        public static void GetEnemyShoot()
        { }

        public static void StartShooting(Connection.SocketClient socketClient)
        {
            do
            {
                Shoot(socketClient);
            } while (WhoWin==0);
            
        }

        public static void Shoot(Connection.SocketClient socketClient)
        {
            int[] cells = new int[2];
            Console.WriteLine("Its your turn: shoot enemy battleship");
            Console.WriteLine("Enter X coordinate");
            string line = Console.ReadLine();
            int value;
            if (int.TryParse(line, out value))
            {
                cells[0] = value;
            }
            else
            {
                throw new Exception("Not an integer!");
            }

            Console.WriteLine("Enter Y coordinate");
            line = Console.ReadLine();
            if (int.TryParse(line, out value))
            {
                cells[1] = value;
            }
            else
            {
                throw new Exception("Not an integer!");
            }

            socketClient.SendMessage(cells[0].ToString() + cells[1].ToString());
            Console.Clear();
            Console.WriteLine("Waiting for reply...");
            while (socketClient.ReceiveMessage() == null) { }
            bool ifweshootagain =  GetYourResult(socketClient.ReceiveMessage().ToString(),cells);
            if (ifweshootagain)
            {
                return;
            }
        }

        public static void CreateBoardTest()
        {
            for (int i = 0; i < 4; i++)
            {
                Myships[1, i+1] = 4;
            }
            for (int i = 3; i < 6; i++)
            {
                Myships[i, 1] = 3;
            }
            for (int i = 6; i < 9; i++)
            {
                Myships[i, 1] = 3;
            }
            for (int i = 0; i < 1; i++)
            {
                Myships[i, 9 - i] = 2;
            }
            for (int i = 0; i < 2; i++)
            {
                Myships[i, 0] = 2;
            }
            for (int i = 0; i < 2; i++)
            {
                Myships[3 + i, 0] = 2;
            }
            for (int i = 0; i < 2; i++)
            {
                Myships[6 + i, 0] = 2;
            }
            for (int i = 0; i < 3; i++)
            {
                Myships[i+4, i+3] = 1;
            }
            DrawBoard();
        }

        public static void StartGame()
        {
            DrawBoard();

            //SetFourCellShip();

            //SetThreeCellShips();

            //SetTwoCellShips();

            //SetOneCellShips();
            CreateBoardTest();
            Console.WriteLine("If u want to be server write 1, if u want to connect to server write 0");
            char a = Console.ReadLine()[0];
            switch (a)
            {
                case '0':
                    Console.WriteLine("Please write IP of device u want to connect formatted like : 127.0.0.1");
                    IPAddress ServerIP;
                    IPAddress.TryParse(Console.ReadLine(), out ServerIP);
                    Console.WriteLine("Please write port of server device");
                    int port;
                    Int32.TryParse(Console.ReadLine(), out port);
                    Connection.SocketClient socketClient = new Connection.SocketClient(ServerIP, port);
                    // Connection.SocketServer socketServer = new Connection.SocketServer(IPAddress.Parse("127.0.0.1"), 11000);
                    StartShooting(socketClient);
                    break;
                case '1':

                    break;
                default:
                    StartGame(Myships);
                    break;
            }
            Console.WriteLine("Wanna play again?");
            Environment.Exit(0);

        }

        public static void StartGame(int[,] Mytable)
        {
            Console.WriteLine("If u want to be server write 1, if u want to connect to server write 0");
            char a = Console.ReadLine()[0];
            switch (a)
            {
                case '0':
                    {
                        Console.WriteLine("Please write IP of device u want to connect formatted like : 127.0.0.1");
                        IPAddress ServerIP;
                        IPAddress.TryParse(Console.ReadLine(), out ServerIP);
                        Console.WriteLine("Please write port of server device");
                        int port;
                        Int32.TryParse(Console.ReadLine(), out port);
                        Connection.SocketClient socketClient = new Connection.SocketClient(ServerIP, port);
                        // Connection.SocketServer socketServer = new Connection.SocketServer(IPAddress.Parse("127.0.0.1"), 11000);
                        StartShooting(socketClient);
                    }
                    break;
                case '1':
                    {
                        Console.WriteLine("Please write IP your computer formatted like : 127.0.0.1");
                        IPAddress ServerIP;
                        IPAddress.TryParse(Console.ReadLine(), out ServerIP);
                        Console.WriteLine("Please write port of server device");
                        int port;
                        Int32.TryParse(Console.ReadLine(), out port);
                        Connection.SocketServer socketServer = new Connection.SocketServer(ServerIP, port);
                    }
                    break;
                default:
                    StartGame(Myships);
                    break;
            }
            Console.WriteLine("Wanna play again?");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            StartGame();

        }
    }
}
