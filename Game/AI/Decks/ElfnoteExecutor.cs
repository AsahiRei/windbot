using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;
using static WindBot.Game.AI.Decks.TimeThiefExecutor;

namespace WindBot.Game.AI.Decks
{
    [Deck("Elfnote", "AI_Elfnote")]
    class ElfnoteExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int VidriumThePowerPatronOfChaosExtermination = 70488851;
            public const int PowerPatronShadowSpiritJunordo = 10266279;
            public const int PowerPatronShadowBeastNervedo = 17473466;
            public const int FidraulisHarmonia = 70088809;
            public const int BystialDruiswurm = 6637331;
            public const int BystialMagnamhut = 33854624;
            public const int ElfnoteLucina = 13597785;
            public const int ElfnoteTinia = 59581480;
            public const int ElfnoteFortuna = 85976588;
            public const int ElfnoteRegina = 56651978;
            public const int FiendsmithEngraver = 60764609;
            public const int MediusThePure = 97556336;
            public const int MulcharmyFuwalos = 42141493;
            public const int AshBlossomJoyousSpring = 14558127;
            public const int GhostBelleHauntedMansion = 73642296;
            public const int MaxxC = 23434538;
            public const int ElfnotePowerPatron = 12375297;
            public const int DrollLockBird = 94145021;
            public const int UnleashedPowerPatronPortalTerminus = 25661743;
            public const int CalledByTheGrave = 24224830;
            public const int ElfnotesWelcomeHome = 64491754;
            public const int ElfnotesRhapsodiaOfMadness = 24092792;
            public const int NervaThePowerPatronOfCreation = 53589300;
            public const int NecroquipPrincess = 93860227;
            public const int ChaosAngel = 22850702;
            public const int JunoraThePowerPatronOfTuning = 5914858;
            public const int BaronneDeFleur = 84815190;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int EnigmasterPackbit = 72444406;
            public const int JunkBerserker = 59771339;
            public const int FADawnDragster = 33158448;
            public const int ElfnoteSeraphimStrelitzia = 42302563;
            public const int GoldenCloudBeastMalong = 93125329;
            public const int ArmadesKeeperOfBoundaries = 88033975;
            public const int DDDWaveHighKingCaesar = 79559912;
            public const int LinkSpider = 98978921;
            public const int FiendsmithsRequiem = 2463794;
        }
        List<int> checkLv6ElfnoteMonster = new List<int>()
        {
            CardId.ElfnoteLucina,
            CardId.ElfnoteTinia,
            CardId.ElfnoteRegina
        };
        public bool uselessSet()
        {
            if (!(Bot.HasInHand(CardId.ElfnoteLucina) ||
                Bot.HasInHand(CardId.ElfnoteTinia) ||
                Bot.HasInHand(CardId.ElfnoteRegina) ||
                Bot.HasInHand(CardId.MediusThePure)))
            {
                return true;
            }
            return false;
        }
        public ElfnoteExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //summon
            AddExecutor(ExecutorType.Summon, CardId.ElfnotePowerPatron, ElfnotePowerPatronSummon);
            AddExecutor(ExecutorType.Summon, CardId.MediusThePure);
            //spsummon
            AddExecutor(ExecutorType.SpSummon, CardId.ElfnoteFortuna, ElfnoteFortunaSpSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.ElfnoteLucina, ElfnoteLucinaSpSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.ElfnoteTinia);
            AddExecutor(ExecutorType.SpSummon, CardId.ElfnoteSeraphimStrelitzia);
            AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon, CrystalWingSynchroDragonSpSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.ArmadesKeeperOfBoundaries);
            AddExecutor(ExecutorType.SpSummon, CardId.FiendsmithsRequiem);
            AddExecutor(ExecutorType.SpSummon, CardId.BaronneDeFleur);
            //activate
            AddExecutor(ExecutorType.Activate, CardId.PowerPatronShadowBeastNervedo);
            AddExecutor(ExecutorType.Activate, CardId.PowerPatronShadowSpiritJunordo);
            AddExecutor(ExecutorType.Activate, CardId.VidriumThePowerPatronOfChaosExtermination);
            AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, ActivateCrystalWingSynchroDragonEffect);
            AddExecutor(ExecutorType.Activate, CardId.AshBlossomJoyousSpring, DefaultAshBlossomAndJoyousSpring);
            AddExecutor(ExecutorType.Activate, CardId.MaxxC, DefaultMaxxC);
            AddExecutor(ExecutorType.Activate, CardId.GhostBelleHauntedMansion, DefaultGhostBelleAndHauntedMansion);
            AddExecutor(ExecutorType.Activate, CardId.DrollLockBird);
            AddExecutor(ExecutorType.Activate, CardId.MulcharmyFuwalos, ActivateMulcharmyFuwalos);
            AddExecutor(ExecutorType.Activate, CardId.ElfnoteTinia, ActivateElfnoteTiniaEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnoteFortuna, ActivateElfnoteFortunaEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnotesWelcomeHome, ActivateElfnotesWelcomeHomeEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnoteRegina, ActivateElfnoteReginaEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnoteLucina, ActivateElfnoteLucinaEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnotePowerPatron, ActivateElfnotePowerPatronEffect);
            AddExecutor(ExecutorType.Activate, CardId.ElfnoteSeraphimStrelitzia, ActivateElfnoteSeraphimStrelitziaEffect);
            AddExecutor(ExecutorType.Activate, CardId.MediusThePure, ActivateMediusThePureEffect);
            AddExecutor(ExecutorType.Activate, CardId.FiendsmithsRequiem, ActivateFiendsmithsRequiemEffect);
            AddExecutor(ExecutorType.Activate, CardId.CalledByTheGrave, DefaultCalledByTheGrave);
            AddExecutor(ExecutorType.Activate, CardId.BystialDruiswurm);
            AddExecutor(ExecutorType.Activate, CardId.BystialMagnamhut);
            AddExecutor(ExecutorType.Activate, CardId.FidraulisHarmonia);
            //AddExecutor(ExecutorType.Activate, CardId.ElfnotesRhapsodiaOfMadness);
            //spell/trap set
            AddExecutor(ExecutorType.SpellSet, CardId.ElfnotesRhapsodiaOfMadness);
            //monster set
            AddExecutor(ExecutorType.MonsterSet, CardId.AshBlossomJoyousSpring, AshBlossomSet);
            AddExecutor(ExecutorType.MonsterSet, CardId.MaxxC, MaxxCSet);
            AddExecutor(ExecutorType.MonsterSet, CardId.GhostBelleHauntedMansion, GhostBelleHauntedMansionSet);
            AddExecutor(ExecutorType.MonsterSet, CardId.MulcharmyFuwalos, MulcharmyFuwalosSet);
        }
        public bool AshBlossomSet()
        {
            return uselessSet();
        }
        public bool MaxxCSet()
        {
            return uselessSet();
        }
        public bool GhostBelleHauntedMansionSet()
        {
            return uselessSet();
        }
        public bool MulcharmyFuwalosSet()
        {
            return uselessSet();
        }
        public bool ActivateMulcharmyFuwalos()
        {
            return Duel.LastChainPlayer == 1;
        }
        public bool ElfnoteFortunaSpSummon()
        {
            return !(Bot.HasInMonstersZone(CardId.ElfnoteLucina) || Bot.HasInHand(CardId.MediusThePure));
        }
        public bool ActivateElfnoteFortunaEffect()
        {
            AI.SelectCard(CardId.ElfnotesRhapsodiaOfMadness);
            return true;
        }
        public bool ActivateElfnoteTiniaEffect()
        {
            AI.SelectCard(CardId.ElfnotesWelcomeHome);
            return true;
        }
        public bool ActivateElfnotesWelcomeHomeEffect()
        {
            if (Bot.HasInMonstersZone(CardId.ElfnoteTinia))
            {
                AI.SelectCard(CardId.ElfnoteTinia);
                AI.SelectNextCard(CardId.ElfnoteRegina);
                return true;
            }
            return false;
        }
        public bool ActivateElfnoteReginaEffect()
        {
            if (Card.IsSpecialSummoned)
            {
                AI.SelectCard(CardId.ElfnoteLucina);
            }
            return true;
        }
        public bool ElfnoteLucinaSpSummon()
        {
            return !Bot.HasInMonstersZone(CardId.ElfnoteTinia);
        }
        public bool ActivateElfnoteLucinaEffect()
        {
            if (!Bot.HasInHand(CardId.ElfnotePowerPatron))
            {
                AI.SelectCard(CardId.ElfnotePowerPatron);
            }
            return true;
        }
        public bool ElfnotePowerPatronSummon()
        {
            return checkLv6ElfnoteMonster.Any(cardId => Bot.HasInMonstersZone(cardId));
        }
        public bool ActivateElfnotePowerPatronEffect()
        {
            if (Card.Location == CardLocation.MonsterZone &&
                (Bot.HasInMonstersZone(CardId.ElfnoteFortuna) ||
                Bot.HasInMonstersZone(CardId.ElfnoteLucina) ||
                Bot.HasInMonstersZone(CardId.ElfnoteTinia)) &&
                Bot.HasInMonstersZone(CardId.ElfnoteSeraphimStrelitzia))
            {
                return true;
            }
            else if (Card.Location == CardLocation.MonsterZone && Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon))
            {
                return true;
            }
            else if (Card.Location != CardLocation.MonsterZone)
            {
                AI.SelectCard(CardId.ElfnoteFortuna);
                return true;
            }
            return false;
        }
        public bool ActivateElfnoteSeraphimStrelitziaEffect()
        {
            if (Bot.HasInHandOrInGraveyard(CardId.ElfnotePowerPatron))
            {
                AI.SelectCard(CardId.ElfnotePowerPatron);
                AI.SelectYesNo(false);
                return true;
            }
            else if (!Bot.HasInMonstersZone(CardId.ElfnoteTinia))
            {
                AI.SelectCard(CardId.ElfnoteRegina);
                return true;
            }
            else if (Bot.HasInHandOrInGraveyard(CardId.ElfnotePowerPatron))
            {
                return true;
            }
            return false;
        }
        public bool CrystalWingSynchroDragonSpSummon()
        {
            AI.SelectPlace(3);
            return true;
        }
        public bool ActivateCrystalWingSynchroDragonEffect()
        {
            return Duel.LastChainPlayer == 1;
        }
        public bool ActivateMediusThePureEffect()
        {
            AI.SelectCard(CardId.ElfnotePowerPatron);
            AI.SelectOption(1);
            return true;
        }
        public bool ActivateFiendsmithsRequiemEffect()
        {
            if (Card.Location == CardLocation.MonsterZone)
            {
                AI.SelectCard(CardId.FiendsmithEngraver);
                AI.SelectPlace(1);
            }
            return true;
        }
    }
}
