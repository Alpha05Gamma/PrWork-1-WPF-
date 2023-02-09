using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pract01___Xs_and_Os
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        private int[,] field = new int[,] { {0,0,0 },{0,0,0 }, {0,0,0 } }; //0-пустота, 1-ПК, 2-Игрок

        private int win;
        private int pc = 2;
        private int xlast;
        private int ylast;
        private int xfir;
        private int yfir;

        private bool payerSide = true;
        private bool pcturn = true;
        private bool userIsFirs = false;
        private bool isFirst = true;
    
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cell_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string tag = button.Tag.ToString();
            string[] index = tag.Split(',');
            int x = Convert.ToInt32(index[0]);
            int y = Convert.ToInt32(index[1]);

            if (field[x,y] == 0)
            {
                field[x, y] = 2;
                xlast = x;
                ylast = y;
                if (isFirst)
                {
                    xfir = x;
                    yfir = y;

                    isFirst= false;
                    userIsFirs = false;
                }
            }

            pcturn = true;
            ButtonLock();
            AI();
            ButtonUnlock();
            paint();
        }

        private void gameStart_Click(object sender, RoutedEventArgs e)
        {
            recet();

            if(!payerSide)
            {
                pc = 1;
                AI();
            }

        }  

        private void AI() 
        {
            stalemate(); 
            offence(); 
            if (win == 0) 
            {
                defence();
                if (pcturn == true) 
                {
                    if (pc == 1) 
                    {
                        kross();
                    }
                    else 
                    {
                        if (xfir == 1 && yfir == 1) 
                        {
                            angle(); 
                        }
                        else 
                        {
                            if (userIsFirs) 
                            {
                                center(); 
                                userIsFirs = false;
                            }
                            else
                            {
                                angle(); 
                            }
                        }
                    }
                } 
                stalemate();
            }
            else
            {
                win = 1;
                paint();
            }
        }

        private void center()
        {
            if (field[1,1] == 0)
            {
                field[1,1] = 1;
            }
        } 

        private void random()
        {
            stalemate();

            if (pcturn)
            {
                int x = rnd.Next(0, 3);
                int y = rnd.Next(0, 3);

                if (field[x, y] == 0)
                {
                    field[x, y] = 1;
                }
                else
                {
                    random();
                }
            }
                       
        }

        private void kross()
        {
            if (xlast == 0 && ylast == 0) //если 0,0
            {
                if (field[2, 2] == 0)
                {
                    field[2, 2] = 1;
                    pcturn = false;
                    paint();
                }
                else
                {
                    random();
                }
            }
            else
            {
                if (xlast == 2 && ylast == 0) //2.0
                {
                    if (field[0, 2] == 0)
                    {
                        field[0, 2] = 1;
                        pcturn = false;
                        paint();
                    }
                    else
                    {
                        random();
                    }
                }
                else
                {
                    if (xlast == 0 && ylast == 2) //0.2
                    {
                        if (field[2, 0] == 0)
                        {
                            field[2, 0] = 1;
                            pcturn = false;
                            paint();
                        }
                        else
                        {
                            random();
                        }
                    }
                    else
                    {
                        if (xlast == 2 && ylast == 2) //2.2
                        {
                            if (field[0, 0] == 0)
                            {
                                field[0, 0] = 1;
                                pcturn = false;
                                paint();
                            }
                            else
                            {
                                random();
                            }
                        }
                        else
                        {
                            if (xlast == 0 && ylast == 1) //0.1
                            {
                                if (field[2, 0] == 0)
                                {
                                    field[2, 0] = 1;
                                    pcturn = false;
                                    paint();
                                }
                                else
                                {
                                    if (field[2, 2] == 0)
                                    {
                                        field[2, 2] = 1;
                                        pcturn = false;
                                        paint();
                                    }
                                    else
                                    {
                                        random();
                                    }
                                }
                            }
                            else
                            {
                                if (xlast == 1 && ylast == 0) //1.0
                                {
                                    if (field[0, 2] == 0)
                                    {
                                        field[0, 2] = 1;
                                        pcturn = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if (field[2, 2] == 0)
                                        {
                                            field[2, 2] = 1;
                                            pcturn = false;
                                            paint();
                                        }
                                        else
                                        {
                                            random();
                                        }
                                    }
                                }
                                else
                                {
                                    if (xlast == 2 && ylast == 1) //2.1
                                    {
                                        if (field[0, 0] == 0)
                                        {
                                            field[0, 0] = 1;
                                            pcturn = false;
                                            paint();
                                        }
                                        else
                                        {
                                            if (field[0, 2] == 0)
                                            {
                                                field[0, 2] = 1;
                                                pcturn = false;
                                                paint();
                                            }
                                            else
                                            {
                                                random();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (xlast == 1 && ylast == 2) //1.2
                                        {
                                            if (field[0, 0] == 0)
                                            {
                                                field[0, 0] = 1;
                                                pcturn = false;
                                                paint();
                                            }
                                            else
                                            {
                                                if (field[2, 0] == 0)
                                                {
                                                    field[2, 0] = 1;
                                                    pcturn = false;
                                                    paint();
                                                }
                                                else
                                                {
                                                    random();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void angle() 
        {
            if (field[0, 0] == 0)
            {
                field[0, 0] = 1;
                pcturn = false;
                paint();
            }
            else
            {
                if (field[2, 0] == 0)
                {
                    field[2, 0] = 1;
                    pcturn = false;
                    paint();
                }
                else
                {
                    if (field[0, 2] == 0)
                    {
                        field[0, 2] = 1;
                        pcturn = false;
                        paint();
                    }
                    else
                    {
                        if (field[2, 2] == 0)
                        {
                            field[2, 2] = 1;
                            pcturn = false;
                            paint();
                        }
                        else
                        {
                            random();
                        }
                    }
                }
            }
        }


        private void offence()
        {
            if (((field[0, 0] + field[0, 1] + field[0, 2]) == 2) && (field[0, 0] == 1 || field[0, 1] == 1 || field[0, 2] == 1))
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[0, j] == 0)
                    {
                        field[0, j] = 1;
                    }
                }
                win = 1;
                paint();
                
            }
            else
            {
                if (((field[1, 0] + field[1, 1] + field[1, 2]) == 2) && (field[1, 0] == 1 || field[1, 1] == 1 || field[1, 2] == 1))
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field[1, j] == 0)
                        {
                            field[1, j] = 1;
                        }
                    }
                    win = 1;
                    paint();
                    
                }
                else
                {
                    if (((field[2, 0] + field[2, 1] + field [2, 2]) == 2) && (field[2, 0] == 1 || field[2, 1] == 1 || field[2, 2] == 1))
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (field[2, j] == 0)
                            {
                                field[2, j] = 1;
                            }
                        }
                        win = 1;
                        paint();
                        
                    }
                    else
                    {
                        if (((field[0, 0] + field[1, 0] + field[2, 0]) == 2) && (field[0, 0] == 1 || field[1, 0] == 1 || field[2, 0] == 1))
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (field[i, 0] == 0)
                                {
                                    field[i, 0] = 1;
                                }
                            }
                            win = 1;
                            paint();
                            
                        }
                        else
                        {
                            if (((field[0, 1] + field[1, 1] + field[2, 1]) == 2) && (field[0, 1] == 1 || field[1, 1] == 1 || field[2, 1] == 1))
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (field[i, 1] == 0)
                                    {
                                        field[i, 1] = 1;
                                    }
                                }
                                win = 1;
                                paint();
                                
                            }
                            else
                            {
                                if (((field[0, 2] + field[1, 2] + field[2, 2]) == 2) && (field[0, 2] == 1 || field[1, 2] == 1 || field[2, 2] == 1))
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (field[i, 2] == 0)
                                        {
                                            field[i, 2] = 1;
                                        }
                                    }
                                    win = 1;
                                    paint();
                                    
                                }
                                else
                                {
                                    if (((field[0, 0] + field[1, 1] + field[2, 2]) == 2) && (field[0, 0] == 1 || field[1, 1] == 1 || field[2, 2] == 1))
                                    {
                                        if (field[0, 0] == 0) { field[0, 0] = 1; }                                            
                                        if (field[1, 1] == 0) { field[1, 1] = 1; }                                            
                                        if (field[2, 2] == 0) { field[2, 2] = 1; }
                                            
                                        win = 1;
                                        paint();
                                        
                                    }
                                    else
                                    {
                                        if (((field[2, 0] + field[1, 1] + field[0, 2]) == 2) && (field[2, 0] == 1 || field[1, 1] == 1 || field[0, 2] == 1))
                                        {
                                            if (field[2, 0] == 0) { field[2, 0] = 1; }
                                                
                                            if (field[1, 1] == 0) { field[1, 1] = 1; }
                                               
                                            if (field[0, 2] == 0) { field[0, 2] = 1; }
                                                
                                            win = 1;
                                            paint();
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void defence()
        {
            if ((field[0, 0] + field[0, 1] + field[0, 2]) == 4 && field[0, 0] != 1 && field[0, 1] != 1 && field[0, 2] != 1) //1-4-7 — защита
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[0, j] == 0)
                    {
                        field[0, j] = 1;
                        pcturn = false;
                        paint();
                    }
                }
            }
            else
            {
                if ((field[1, 0] + field[1, 1] + field[1, 2]) == 4 && field[1, 0] != 1 && field[1, 1] != 1 && field[1, 2] != 1) //2-5-8 — защита
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (field[1, j] == 0)
                        {
                            field[1, j] = 1;
                            pcturn = false;
                            paint();
                        }
                    }
                }
                else
                {
                    if ((field[2, 0] + field[2, 1] + field[2, 2]) == 4 && field[2, 0] != 1 && field[2, 1] != 1 && field[2, 2] != 1) //3-6-9 — защита
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (field[2, j] == 0)
                            {
                                field[2, j] = 1;
                                pcturn = false;
                                paint();
                            }
                        }
                    }
                    else
                    {
                        if ((field[0, 0] + field[1, 0] + field[2, 0]) == 4 && field[0, 0] != 1 && field[1, 0] != 1 && field[2, 0] != 1) //1-2-3 — защита
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (field[i, 0] == 0)
                                {
                                    field[i, 0] = 1;
                                    pcturn = false;
                                    paint();
                                }
                            }
                        }
                        else
                        {
                            if ((field[0, 1] + field[1, 1] + field[2, 1]) == 4 && field[0, 1] != 1 && field[1, 1] != 1 && field[2, 1] != 1) //4-5-6 — защита
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (field[i, 1] == 0)
                                    {
                                        field[i, 1] = 1;
                                        pcturn = false;
                                        paint();
                                    }
                                }
                            }
                            else
                            {
                                if ((field[0, 2] + field[1, 2] + field[2, 2]) == 4 && field[0, 2] != 1 && field[1, 2] != 1 && field[2, 2] != 1) //7-8-9 — защита
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (field[i, 2] == 0)
                                        {
                                            field[i, 2] = 1;
                                            pcturn = false;
                                            paint();
                                        }
                                    }
                                }
                                else
                                {
                                    if ((field[0, 0] + field[1, 1] + field[2, 2]) == 4 && field[0, 0] != 1 && field[1, 1] != 1 && field[2, 2] != 1) //1-5-9 — защита
                                    {
                                        if (field[0, 0] == 0) { field[0, 0] = 1; }                                           
                                        if (field[1, 1] == 0) { field[1, 1] = 1; }                              
                                        if (field[2, 2] == 0) { field[2, 2] = 1; }
                                            
                                        pcturn = false;
                                        paint();
                                    }
                                    else
                                    {
                                        if ((field[2, 0] + field[1, 1] + field[0, 2]) == 4 && field[2, 0] != 1 && field[1, 1] != 1 && field[0, 2] != 1) //3-5-7 — защита
                                        {
                                            if (field[2, 0] == 0) { field[2, 0] = 1; }                                                
                                            if (field[1, 1] == 0) { field[1, 1] = 1; }                                                
                                            if (field[0, 2] == 0) { field[0, 2] = 1; }
                                                
                                            pcturn = false;
                                            paint();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void paint()
        {
            Button[] buttons = new Button[] {cell00,cell01,cell02,cell03,cell04,cell05,cell06,cell07,cell08};

            foreach (Button button in buttons)
            {
                string tag = button.Tag.ToString();
                string[] index = tag.Split(',');
                int x = Convert.ToInt32(index[0]);
                int y = Convert.ToInt32(index[1]);

                switch(field[x,y])
                {
                    case 0:
                        button.Content = " ";
                        button.IsEnabled = true; 
                        break;

                    case 2:
                        if (payerSide)
                        {
                            button.Content = "X";
                            button.IsEnabled = false;
                        }
                        else
                        {
                            button.Content = "O";
                            button.IsEnabled = false;
                        }
                        break;

                    case 1:
                        if (!payerSide)
                        {
                            button.Content = "X";
                            button.IsEnabled = false;
                        }
                        else
                        {
                            button.Content = "Y";
                            button.IsEnabled = false;
                        }
                        break;
                }
            }

            if(win == 1)
            {
                WinnerBox.IsEnabled = true;
                WinnerBox.Text = "Вы проиграли";
                ButtonLock();
            }
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if(box.SelectedIndex == 0)
            {
                payerSide= true;
            }
            else
            {
                payerSide= false;
            }
        }

        private void stalemate()
        {
            int couter = 0;

            foreach(int integer in field)
            {
                if (integer != 0) { couter += 1; }
            }

            if(couter > 8)
            {
                WinnerBox.IsEnabled = true;
                WinnerBox.Text = "Ничья";

                for(int i = 0; i < 3; i++)
                {
                    for(int j =0; j < 3; j++)
                    {
                        if (field[i, j] == 0) { field[i, j] = 1; }
                    }
                }

                paint();

                pcturn = false;
            }
        }

        private void ButtonLock()
        {
            Button[] buttons = new Button[] { cell00, cell01, cell02, cell03, cell04, cell05, cell06, cell07, cell08 };

            foreach(Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void ButtonUnlock()
        {
            Button[] buttons = new Button[] { cell00, cell01, cell02, cell03, cell04, cell05, cell06, cell07, cell08 };

            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
            }
        }

        private void recet()
        {
            Button[] buttons = new Button[] { cell00, cell01, cell02, cell03, cell04, cell05, cell06, cell07, cell08 };

            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
            }

            WinnerBox.IsEnabled = false;
            WinnerBox.Text = " ";

            if (payerSide)
            {
                isFirst = true;
                userIsFirs = false;
                pc = 2;
            }
            else
            {
                isFirst = true;
                userIsFirs = true;
                pc = 1;
            }

            win = 0;

            field = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            paint();
        }
    }
}
