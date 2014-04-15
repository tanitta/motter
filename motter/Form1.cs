using System;
using System.Drawing;
using System.Windows.Forms;
namespace motter
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.TextBox textBox;
        System.Windows.Forms.Label words;
        bool writed = false;
        DateTime timeNow = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
            textBox = new System.Windows.Forms.TextBox()
            {
                Dock = DockStyle.Fill,        // 結合スタイル
                ScrollBars = ScrollBars.Both, // スクロールバー
                Multiline = true,             // 複数行
                WordWrap = false,             // 折り返し
                Font = new Font("ＭＳ ゴシック", 10, FontStyle.Regular),
                MaxLength = 140,
            };

            words = new System.Windows.Forms.Label() {
                Location = new Point(10, 20),
                AutoSize = true,
            };

            this.Controls.Add(textBox);

            this.Text = "monotter";

            words.Text = "words:" + textBox.TextLength.ToString();
            this.Controls.Add(words);
            this.textBox.TextChanged += new EventHandler(currencyTextBox_TextChanged);
            this.textBox.KeyPress += new KeyPressEventHandler(textWrite);
        }

        // KeyPress イベントのイベントハンドラ
        private void currencyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (writed) {
                this.textBox.Text = string.Empty;
            }
            writed = false;
            //words.Text = "words:" + textBox.TextLength.ToString();
            //words.Text = System.Environment.CurrentDirectory;
        }

        private void textWrite(object sender, KeyPressEventArgs e) {
            //どの修飾子キー(Shift、Ctrl、およびAlt)が押されているか
            if (Control.ModifierKeys == Keys.Shift && e.KeyChar == (char)Keys.Return)
            {
                words.Text += "Enter+Shiftキーが押されています。";

                string currentDirec = System.Environment.CurrentDirectory;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                @currentDirec+"\\Log.txt",
                true,
                System.Text.Encoding.GetEncoding("shift_jis"));
                sw.Write(timeNow.ToString("yyyy/MM/dd HH:mm:ss") +":"+ textBox.Text+"\n");
                //閉じる
                sw.Close();
                this.textBox.Text = string.Empty;
                writed = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
