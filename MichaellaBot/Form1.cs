using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace MichaellaBot
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
            Choices choices = new Choices();
            String[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);
            speech.SelectVoiceByHints(VoiceGender.Female);
        }

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;
            //if (result == "Hello")
            //{
            //    result = "My name is Michealla";
            //    speech.SpeakAsync(result);
            //    label2.Text = result;
            //}
            switch (result)
            {
                case "Hello": result = "My name is Victoria, how can i help you";
                    speech.SpeakAsync(result);
                    label2.Text = result;
                    break;
                case "What is today":
                    result = "The day is "+DateTime.Now.ToShortDateString();
                    speech.SpeakAsync(result);
                    label2.Text = result;
                    break;
                case "Thank you":
                    result = "You are welcome";
                    speech.SpeakAsync(result);
                    label2.Text = result;
                    break;
                case "Close":
                    result = "GoodBye";
                    speech.SpeakAsync(result);
                    label2.Text = result;
                    Application.Exit();
                    break;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
