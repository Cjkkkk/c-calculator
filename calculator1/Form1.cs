using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator1
{
    public partial class Form1 : Form
    {
        int length;
        const int size = 20;
        string[] element = new string[size];//字符串转字符串型数组中缀表达式
        string[] pastfix = new string[size];//中缀转后缀
        string[] opstack = new string[size];//符号站
        int[] type = new int[size];//优先级站
        double[] calculatorstack = new double[size];//计算站
        int op = -1;


        public Form1()
        {
            InitializeComponent();
            foreach (Control i in Controls)
            {
                if( i.TabIndex >=0 && i.TabIndex<=9)
                {
                    i.Click += delegate
                    {
                        textBox1.Text = textBox1.Text + i.Text;
                    };
                }
                if (i.TabIndex >= 10 && i.TabIndex <= 13)
                {
                    i.Click += delegate
                    {
                        textBox1.Text = textBox1.Text +" "+ i.Text +" ";
                    };
                }

            }

        }
        //清空
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
           
        }
        //计算
        public int isop (char a)
        {
            if (a == '+'|| a == '-') return 0;
            else if (a == '*'|| a == '/') return 1;
            else return 4;
           
        }
        public void dividestring(string currentstring)
        {
            int i = 0;
            int j = 0;

            while (i <= currentstring.Length - 1)
            {

                if (isop(currentstring[i]) != 4)
                {
                    element[j] = currentstring.Substring(0, i);
                    type[j] = 4;
                    j++;
                    element[j] = currentstring[i].ToString();
                    type[j] = isop(currentstring[i]);
                    currentstring = currentstring.Substring(i + 1);
                    j++;
                    i = 0;
                }

                else
                {
                    i++;
                }
            }
            if (currentstring != "")
            {
                element[j] = currentstring;
                type[j] = 4;
                length = j;
            }
          
        }


        public void infixtopastfix()
        {
            int i;
            int j = 0;
            for (i = 0; i <= length; i++)
            {
                if (type[i] == 4)
                {
                    pastfix[j] = element[i];
                    type[j] = 4;
                    j++;
                }
                else
                {
                    if(op == -1)
                    {
                        op++;
                        opstack[op] = element[i];
                    }
                    else
                    {
                        while (isop((opstack[op])[0]) >= isop((element[i])[0]))
                        {
                            pastfix[j] = opstack[op];
                            type[j] = isop((opstack[op])[0]);
                            op--;
                            j++;
                            if (op == -1)
                            {
                                break;
                            }
                        }
                        op++;
                        opstack[op] = element[i];

                    }
                }
            }
            while(op != -1)
            {
                pastfix[j] = opstack[op];
                type[j] = isop((opstack[op])[0]);
                op--;
                j++;
            }
          
           // for(i = 0; i <= j - 1; i++)
            //{
             //   MessageBox.Show(pastfix[i]);
               // MessageBox.Show(type[i].ToString());
          //  }

        }

        public void calculatorpastfix()
        {
            //把数字压入战中
            op = -1;
            int i;
          /*  for (i = 0; i <= length; i++)
            {
                MessageBox.Show(pastfix[i]);
               // MessageBox.Show(type[i].ToString());

            }*/
            for (i = 0; i <= length; i++)
            {


                if (type[i] == 4)
                {
                    op++;
                    calculatorstack[op] = Convert.ToDouble(pastfix[i]);

                }



                //MessageBox.Show(op.ToString());

                
                else
                {
                    if (pastfix[i] == "+")
                    {
                        
                        calculatorstack[op - 1]=calculatorstack[op-1] + calculatorstack[op];
                        op--;
                    }
                    else if (pastfix[i] == "-")
                    {  
                        calculatorstack[op - 1] = calculatorstack[op-1] - calculatorstack[op];
                        op--;
                    }
                    else if (pastfix[i] == "*")
                    {
                        calculatorstack[op - 1] = calculatorstack[op-1]* calculatorstack[op];
                        op--;
                    }
                    else if (pastfix[i] == "/")
                    {
                        if(calculatorstack[op] == 0)
                        {
                            MessageBox.Show("error");
                        }
                        calculatorstack[op - 1] = calculatorstack[op-1] / calculatorstack[op];
                        op--;
                    }
                }
            }
            textBox1.Text = calculatorstack[op].ToString();
            length = 0;
            op = -1;
            //Array.Clear(element, 0, size);
           // Array.Clear(pastfix, 0, size);
           // Array.Clear(type, 0, size);


        }
        public void findnumber (string currentstring)
        {
            currentstring = currentstring.Replace(" ", "");
            textBox1.Text = currentstring;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            findnumber(textBox1.Text);
            dividestring(textBox1.Text);
            infixtopastfix();
            calculatorpastfix();
        }
    }
}
