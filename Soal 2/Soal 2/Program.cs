namespace Soal_2
{
    public enum KarakterState { Berdiri, Tengkurap, Terbang, Jongkok };

    public enum Trigger { TombolW, TombolS, TombolX };

    class PosisiKarakterGame
    {
        public KarakterState nextState;
        public KarakterState prevState;
        public KarakterState currentState;
        public Trigger trigger;

        public PosisiKarakterGame(KarakterState prevState, KarakterState nextState, Trigger trigger)
        {

            this.nextState = nextState; 
            this.prevState = prevState;
            this.trigger = trigger;
        }

        private static PosisiKarakterGame[] transitions =
        {
        new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Terbang, Trigger.TombolW),
        new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Jongkok, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Terbang, KarakterState.Berdiri, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Terbang, KarakterState.Jongkok, Trigger.TombolX),
        new PosisiKarakterGame(KarakterState.Jongkok, KarakterState.Berdiri, Trigger.TombolW),
        new PosisiKarakterGame(KarakterState.Jongkok, KarakterState.Tengkurap, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Tengkurap, KarakterState.Jongkok, Trigger.TombolW),
    };

        public KarakterState getNextState(KarakterState prevState, Trigger trigger)
        {
            KarakterState nextState = prevState;

            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].prevState == prevState && transitions[i].trigger == trigger)
                {
                    nextState = transitions[i].nextState;
                }
            }
            return nextState;
        }

        public void activeTrigger(Trigger trigger)
        {
            KarakterState nextState = getNextState(currentState, trigger);
            this.currentState = nextState;

            if (trigger == null)
            {
                Console.WriteLine("posisi standby");
            }
            if (trigger == Trigger.TombolX)
            {
                Console.WriteLine("posisi istirahat");
            }
        }
    }

    public class main
    {
        public static void Main(string[] args)
        {
            PosisiKarakterGame karakter = new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Berdiri, Trigger.TombolS);
            karakter.currentState = KarakterState.Berdiri;

            Console.WriteLine($"Karakter saat ini {Enum.GetName(typeof(KarakterState), karakter.currentState)}");
            Console.WriteLine("Tombol yang tersedia TombolW, TombolS, TombolX, EXIT");
            Console.Write("Pilih tombol: ");
            String tombol = Console.ReadLine();

            while (tombol != "KELUAR")
            {
                if (Enum.TryParse<Trigger>(tombol, out Trigger trigger))
                {
                    karakter.activeTrigger(trigger);
                }
                else
                {
                    Console.WriteLine("Tombol tidak valid");
                }
                Console.Write("Pilih tombol: ");
                tombol = Console.ReadLine();
            }

        }
    }
}
