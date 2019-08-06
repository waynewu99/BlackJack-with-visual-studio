using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] point = new int[52];                                                                  //卡牌編號
        string[] card =  {"A","A","A","A","2","2","2","2","3","3","3","3","4","4","4","4",          //卡牌內容/點數
            "5","5","5","5","6","6","6","6","7","7","7","7","8","8","8","8",
        "9","9","9","9","10","10","10","10","10","10","10","10","10","10","10","10","10","10","10","10"};
        string[] cardimage = new string[53];                                                        //卡牌圖片位址
        int cardcount = 0;                                                                          //發至第幾張牌
        int[] hostcard = new int [11];                                                              //莊家持有牌
        int[] playercard = new int[11];                                                             //玩家持有牌
        int playerpoint = 0;                                                                        //玩家持有點數
        int realhostpoint = 0;
        int hostpoint = 0;                                                                          //莊家持有點數
        int playergct = 0;                                                                          //玩家取牌次數
        int hostgct = 0;                                                                            //莊家取牌次數
        float rate = 0f;
        float TotalMoney = 500f;
        float betin = 10;
        private void Start_Click(object sender, EventArgs e)
        {
            if (cardcount > 51)
            {
                redo();
            }

            if (hostgct == 0)
            {

                playergetcard();
                hostgetcard();
                playergetcard();
                hostgetcard();
                if (playerpoint == 21)
                {
                    EndTurn();
                }
                Confirm.Enabled = true;
            }
            else
            {
                playergetcard();
                if (playergct > 1)
                {
                    MoreBet.Enabled = false;
                }
                if (playerpoint > 21 || playerpoint == 21)
                {
                    EndTurn();
                }
            }
        }

        private void MoreBet_Click(object sender, EventArgs e)
        {
            betin = betin + 10;
            Bet.Text = betin.ToString();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            for (; realhostpoint <= 17;)
            {
                hostgetcard();
            }
            EndTurn();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 52; i++)
            {
                cardimage[i] = @"Image\g" + i.ToString() + ".jpg";
            }
            for (int i = 0; i <= 51; i++)
            {
                point[i] = i;
            }
            redo();
            Start.Enabled = true;
            MoreBet.Enabled = true;
            Confirm.Enabled = false;
            NextRound.Enabled = false;
            WinOrLose.Enabled = false;
            Bet.Text = betin.ToString();
            Money.Text = TotalMoney.ToString();
            Rate.Text = rate.ToString();
        }

        public void redo()                                                          //洗牌
        {
            Random r = new Random();
            for (int i = 0; i < 52; i++)
            {
                int arr = r.Next(52);
                int temp = point[i];
                point[i] = point[arr];
                point[arr] = temp;
            }
            cardcount = 0;
           
        }

        public void playergetcard()                                                 //玩家取牌
        {
            int temp = 0;
            playercard[playergct] = point[cardcount];
            showplayercard();
            if(card[playercard[playergct]] == "A")
            {
                if(playerpoint < 11)
                {
                    temp = 11;
                }else { temp = 1; }
            }else
            {
                temp = int.Parse(card[playercard[playergct]]);
            }
            playerpoint = playerpoint + temp;
            if (playerpoint > 21)
            { for (int i = 0; i <= playergct; i++)
                {
                    if (card[playercard[i]] == "A")
                    {
                        playercard[i] = 4;
                        playerpoint = playerpoint - 10;
                        return;
                    }
                }
            }
            playertotal.Text = playerpoint.ToString();
            cardcount++;
            playergct++;
        }
        public void hostgetcard()                                                   //莊家取牌
        {
            int temp = 0;
            hostcard[hostgct] = point[cardcount];
            showhostcard();
            if(hostgct == 0)
            {
                if (card[hostcard[hostgct]] == "A")
                {
                    realhostpoint = 1;
                }
                else
                {
                    realhostpoint = int.Parse(card[hostcard[hostgct]]);
                }
            }
            if (hostgct > 0)
            {
                if (card[hostcard[hostgct]] == "A")
                {
                    if (hostpoint < 11)
                    {
                        temp = 11;
                    }
                    else { temp = 1; }
                }
                else
                {
                    temp = int.Parse(card[hostcard[hostgct]]);
                }
            }
            hostpoint = hostpoint + temp;
            realhostpoint = realhostpoint + temp;
            if (realhostpoint > 21)
            {
                for (int i = 0; i <= hostgct; i++)
                {
                    if (card[hostcard[i]] == "A")
                    {
                        hostcard[i] = 4;
                        realhostpoint = realhostpoint - 10;
                        return;
                    }
                }
            }
            if (hostpoint > 21)
            {
                for (int i = 0; i <= hostgct; i++)
                {
                    if (card[hostcard[i]] == "A")
                    {
                        hostcard[i] = 4;
                        hostpoint = hostpoint - 10;
                        return;
                    }
                }
            }
            hosttotal.Text = "??? + " + hostpoint.ToString();
            cardcount++;
            hostgct++;
        }
        public void showplayercard()
        {
                if (playergct == 0)
                {
                    player1.Image = Image.FromFile(@cardimage[playercard[0]]);
                }else if (playergct == 1)
                {
                    player2.Image = Image.FromFile(@cardimage[playercard[1]]);
                }
                else if (playergct == 2)
                {
                    player3.Image = Image.FromFile(@cardimage[playercard[2]]);
                }
                else if (playergct == 3)
                {
                    player4.Image = Image.FromFile(@cardimage[playercard[3]]);
                }
                else if (playergct == 4)
                {
                    player5.Image = Image.FromFile(@cardimage[playercard[4]]);
                }
                else if (playergct == 5)
                {
                    player6.Image = Image.FromFile(@cardimage[playercard[5]]);
                }
                else if (playergct == 6)
                {
                    player7.Image = Image.FromFile(@cardimage[playercard[6]]);
                }
                else if (playergct == 7)
                {
                    player8.Image = Image.FromFile(@cardimage[playercard[7]]);
                }
                else if (playergct == 8)
                {
                    player9.Image = Image.FromFile(@cardimage[playercard[8]]);
                }
                else if (playergct == 9)
                {
                    player10.Image = Image.FromFile(@cardimage[playercard[9]]);
                }
                else if (playergct == 10)
                {
                    player11.Image = Image.FromFile(@cardimage[playercard[10]]);
                }

            
        }
        public void showhostcard()
        {
            if (hostgct == 0)
            {
                host1.Image = Image.FromFile(@cardimage[52]);
            }
            else if (hostgct == 1)
            {
                host2.Image = Image.FromFile(@cardimage[hostcard[1]]);
            }
            else if (hostgct == 2)
            {
                host3.Image = Image.FromFile(@cardimage[hostcard[2]]);
            }
            else if (hostgct == 3)
            {
                host4.Image = Image.FromFile(@cardimage[hostcard[3]]);
            }
            else if (hostgct == 4)
            {
                host5.Image = Image.FromFile(@cardimage[hostcard[4]]);
            }
            else if (hostgct == 5)
            {
                host6.Image = Image.FromFile(@cardimage[hostcard[5]]);
            }
            else if (hostgct == 6)
            {
                host7.Image = Image.FromFile(@cardimage[hostcard[6]]);
            }
            else if (hostgct == 7)
            {
                host8.Image = Image.FromFile(@cardimage[hostcard[7]]);
            }
            else if (hostgct == 8)
            {
                host9.Image = Image.FromFile(@cardimage[hostcard[8]]);
            }
            else if (hostgct == 9)
            {
                host10.Image = Image.FromFile(@cardimage[hostcard[9]]);
            }
            else if (hostgct == 10)
            {
                host11.Image = Image.FromFile(@cardimage[hostcard[10]]);
            }


        }

        private void NextRound_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            player1.Image = null;
            player2.Image = null;
            player3.Image = null;
            player4.Image = null;
            player5.Image = null;
            player6.Image = null;
            player7.Image = null;
            player8.Image = null;
            player9.Image = null;
            player10.Image = null;
            player11.Image = null;

            host1.Image = null;
            host2.Image = null;
            host3.Image = null;
            host4.Image = null;
            host5.Image = null;
            host6.Image = null;
            host7.Image = null;
            host8.Image = null;
            host9.Image = null;
            host10.Image = null;
            host11.Image = null;

            for(int i = 0; i < 11; i++)
            {
                playercard[i] = 0;
                hostcard[i] = 0;
            }                                                     
            playerpoint = 0;                                                                       
            hostpoint = 0;
            realhostpoint = 0;
            playergct = 0;                                                                         
            hostgct = 0;
            WinOrLose.Text = "";
            WinOrLose.Enabled = false;
        }

        public void EndTurn()
        {
            Start.Enabled = false;
            NextRound.Enabled = true;
            host1.Image = Image.FromFile(@cardimage[hostcard[0]]);
            hosttotal.Text = realhostpoint.ToString();
            if (playerpoint == 21)
            {
                WinOrLose.Enabled = true;
                WinOrLose.Text ="玩家21點\n" + "YOU WIN";
                rate = 1.5f;
            }
            else if (playerpoint > 21)
            {
                WinOrLose.Enabled = true;
                WinOrLose.Text = "超過21點\n" + "YOU LOSE";
                rate = -1f;
            }
            
            else if (realhostpoint > 21)
            {
                WinOrLose.Enabled = true;
                WinOrLose.Text = "莊家超過21點\n" + "YOU WIN";
                rate = 1f;
            }
            else if(realhostpoint <= 21 )
            {
                if (realhostpoint == 21 && playerpoint < 21)
                {
                    WinOrLose.Enabled = true;
                    WinOrLose.Text = " 莊家21點\n" + "YOU LOSE";
                    rate = -1f;
                }
                else if (playerpoint > realhostpoint && realhostpoint < 21)
                {
                    WinOrLose.Enabled = true;
                    WinOrLose.Text = "玩家點數大於莊家\n" + "YOU WIN";
                    rate = 1f;
                }
                else if (playerpoint == realhostpoint && realhostpoint < 21)
                {
                    WinOrLose.Enabled = true;
                    WinOrLose.Text =  "玩家點數等於莊家\n" + "TIED";
                    rate = 0f;
                }
                else if (playerpoint < realhostpoint && realhostpoint < 21)
                {
                    WinOrLose.Enabled = true;
                    WinOrLose.Text = "玩家點數小於莊家\n" + "YOU LOSE";
                    rate = -1f;
                }


            }
            betin = 10;
            TotalMoney = betin * rate + TotalMoney;
            Money.Text = TotalMoney.ToString();
            Rate.Text = rate.ToString();
            Confirm.Enabled = false;
        }
    }
}
