<Query Kind="Program" />

static Dictionary<ulong, KingAttack> kingAttacks = new Dictionary<ulong, KingAttack>();

void Main()
{
	kingAttacks.Clear(); //make sure this isn't loaded before running for repeated running
	int boards = 1;
	string line;
	int rowCount = 0;
	
	Stopwatch numberOfTicks = Stopwatch.StartNew();
	InitializeKings();
	
	using (StreamWriter fileWriter = new StreamWriter(@"E:\ProgrammingChallenge\ProgrammingChallenge6\Kim\results.txt"))
	{
		using (StreamReader fileReader = new StreamReader(@"E:\ProgrammingChallenge\ProgrammingChallenge6\540Boards.txt"))
		{
			Board testBoard = new Board(boards);
			while ((line = fileReader.ReadLine()) != null)
			{
				if (line.Equals(string.Empty))
				{
					//testBoard.ProcessBoard().Dump(); 
					fileWriter.WriteLine(string.Format("Game #{0}: {1} king is in check", boards, testBoard.ProcessBoard()));
					//reset for next board
					testBoard = new Board(++boards);
					rowCount = 0;				
					continue;
				}
				
				if (!line.Equals("........"))
					testBoard.AddLine(line, rowCount);
				++rowCount;
			}
		}
		
	}	
	numberOfTicks.Stop();
	numberOfTicks.ElapsedMilliseconds.Dump("Milliseconds");
	numberOfTicks.ElapsedTicks.Dump("Ticks");
}
	
// Define other methods and classes here
struct KingAttack
{
	public ulong diagonalLeftUp;
	public ulong diagonalRightUp;
	public ulong diagonalLeftDown;
	public ulong diagonalRightDown;
	public ulong horizontalLeft;
	public ulong horizontalRight;
	public ulong verticalUp;
	public ulong verticalDown;
	public ulong knight;
	public ulong pawnLower;
	public ulong pawnUpper;
	
	public KingAttack(ulong dlu, ulong dru, ulong dld, ulong drd, ulong hl, ulong hr, ulong vu, ulong vd, ulong k, ulong pl, ulong pu)
	{
		diagonalLeftUp = dlu;
		diagonalRightUp = dru;
		diagonalLeftDown = dld;
		diagonalRightDown = drd;
		horizontalLeft = hl;
		horizontalRight = hr;
		verticalUp = vu;
		verticalDown = vd;
		knight = k;
		pawnLower = pl;
		pawnUpper = pu;
	}
}

struct Piece
{
	public ulong locationOnBoard;
	public int lc;
	public int typeOfPiece;
	/* piece values
		lower
		pawn = 9
		knight = 10
		king = 11
		bishop = 13
		rook = 14
		queen = 15
	
		Upper
		pawn = 1
		knight = 2
		king = 3
		bishop = 5
		rook = 6
		queen = 7
		*/
	
	public Piece(char c, int l)
	{
		lc = l;
		locationOnBoard = (ulong)1 << l; //shift to the appropriate bit
		switch (c)
		{
			case 'p': //pawn
				typeOfPiece = 9;
				break;
			case 'P': //pawn
				typeOfPiece = 1;
				break;
			case 'q': //queen
				typeOfPiece = 15;
				break;
			case 'Q': //queen
				typeOfPiece = 7;
				break;
			case 'n': //knight
				typeOfPiece = 10;
				break;
			case 'N': //knight
				typeOfPiece = 2;
				break;
			case 'r': //rook
				typeOfPiece = 14;
				break;
			case 'R': //rook
				typeOfPiece = 6;
				break;
			case 'b': //bishop
				typeOfPiece = 13;
				break;
			case 'B': //bishop
				typeOfPiece = 5;
				break;
			case 'k': //King
				typeOfPiece = 11;
				break;
			case 'K': //King
				typeOfPiece = 3;
				break;
			default:
				typeOfPiece = 0;
				break;
		}
	}
}



public class Board
{
	int boardNumber;
	ulong locationOfKingLower;
	ulong locationOfKingUpper;
	int countDiagonalLower;
	int countDiagonalUpper;
	int countHVLower;
	int countHVUpper;
	int countKnightLower;
	int countKnightUpper;
	int countPawnLower;
	int countPawnUpper;
	List<Piece> pieces = new List<Piece>();
	public enum BoardResult
	{
		no,
		black,
		white
	}
	
	public Board()
	{
	
	}
	
	public Board(int index)
	{
		boardNumber = index;
	}

	public void AddLine(string line, int shift)
	{
		int location = (shift * 8);
		for (int i = 0; i < 8; i++)
		{
			switch (line[i])
			{
				case '.':
					location++;
					continue;
				case 'p': //pawn
					countPawnLower++;
					break;
				case 'P': //pawn
					countPawnUpper++;
					break;
				case 'q': //queen
					countDiagonalLower++;
					countHVLower++;
					break;
				case 'Q': //queen
					countDiagonalUpper++;
					countHVUpper++;
					break;
				case 'n': //knight
					countKnightLower++;
					break;
				case 'N': //knight
					countKnightUpper++;
					break;
				case 'r': //rook
					countHVLower++;
					break;
				case 'R': //rook
					countHVUpper++;
					break;
				case 'b': //bishop
					countDiagonalLower++;
					break;
				case 'B': //bishop
					countDiagonalUpper++;
					break;
				case 'k': //King
					locationOfKingLower = (ulong)1 << location;
					break;
				case 'K': //King
					locationOfKingUpper = (ulong)1 << location;
					break;
			}
			pieces.Add(new Piece(line[i],location++));
		}
	}
	public Board.BoardResult ProcessBoard()
	{
		//pieces.Dump();
		KingAttack localAttack = kingAttacks[locationOfKingLower];
		int closestPiece = 0;
		
		if (countDiagonalUpper > 0)
		{
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.diagonalRightDown) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if ( closestPiece.Equals(7) || closestPiece.Equals(5) )
			{
				//upper, diagonal&sliding
				return BoardResult.black;
			}
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.diagonalLeftDown) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if ( closestPiece.Equals(7) || closestPiece.Equals(5) )
			{
				//upper, diagonal&sliding
				return BoardResult.black;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.diagonalRightUp) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();

			if ( closestPiece.Equals(7) || closestPiece.Equals(5) )
			{
				//upper, diagonal&sliding
				return BoardResult.black;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.diagonalLeftUp) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if (  closestPiece.Equals(7) || closestPiece.Equals(5) )
			{
				//upper, diagonal&sliding
				return BoardResult.black;
			}
		}	
		
		if ( countHVUpper > 0 )
		{
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.verticalDown) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			
			if ( closestPiece.Equals(7) || closestPiece.Equals(6) )
			{
				//upper, HV&sliding
				return BoardResult.black;
			}
			
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.verticalUp) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if ( closestPiece.Equals(7) || closestPiece.Equals(6) )
			{
				//upper, HV&sliding
				return BoardResult.black;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.horizontalRight) > 0)
				orderby possiblePieces.locationOnBoard 
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if ( closestPiece.Equals(7) || closestPiece.Equals(6) )
			{
				//upper, HV&sliding
				return BoardResult.black;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack.horizontalLeft) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if ( closestPiece.Equals(7) || closestPiece.Equals(6) )
			{
				//upper, HV&sliding
				return BoardResult.black;
			}
		}

		if (countKnightUpper > 0)
		{
			//knight checks
			if ((from possiblePieces in pieces
				where (possiblePieces.typeOfPiece == 2) &&
					((possiblePieces.locationOnBoard & localAttack.knight) > 0)
				select possiblePieces.typeOfPiece).Count() > 0)
					return BoardResult.black;
		}
		
		if (countPawnUpper > 0)
		{
			//check possible attack pawns
			if ((from possiblePieces in pieces
				where (possiblePieces.typeOfPiece == 1) &&
					((possiblePieces.locationOnBoard & localAttack.pawnLower) > 0)
				select possiblePieces.typeOfPiece).Count() > 0)
					return BoardResult.black;
		}

		//check other side
		KingAttack localAttack2 = kingAttacks[locationOfKingUpper];
		if (countDiagonalLower > 0)
		{
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.diagonalRightDown) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if (closestPiece.Equals(15) || closestPiece.Equals(13))
			{
				//lower, diagonal&sliding
				return BoardResult.white;
			}
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.diagonalLeftDown) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if ( closestPiece.Equals(15) || closestPiece.Equals(13))
			{
				//lower, diagonal&sliding
				return BoardResult.white;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.diagonalRightUp) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();

			if ( closestPiece.Equals(15) || closestPiece.Equals(13))
			{
				//lower, diagonal&sliding
				return BoardResult.white;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.diagonalLeftUp) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if (closestPiece.Equals(15) || closestPiece.Equals(13))
			{
				//lower, diagonal&sliding
				return BoardResult.white;
			}
		}
		
		if ( countHVLower > 0 )
		{
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.verticalDown) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			
			if ( closestPiece.Equals(15) || closestPiece.Equals(14))
			{
				//lower, HV&sliding
				return BoardResult.white;
			}
			
			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.verticalUp) > 0)
				select possiblePieces.typeOfPiece).FirstOrDefault();
				
			if ( closestPiece.Equals(15) || closestPiece.Equals(14))
			{
				//lower, HV&sliding
				return BoardResult.white;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.horizontalRight) > 0)
				orderby possiblePieces.locationOnBoard 
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if ( closestPiece.Equals(15) || closestPiece.Equals(14)) 
			{
				//lower, HV&sliding
				return BoardResult.white;
			}

			closestPiece = (from possiblePieces in pieces
				where ((possiblePieces.locationOnBoard & localAttack2.horizontalLeft) > 0)
				orderby possiblePieces.locationOnBoard descending
				select possiblePieces.typeOfPiece).FirstOrDefault();
			if (closestPiece.Equals(15) || closestPiece.Equals(14)) 
			{
				//lower, HV&sliding
				return BoardResult.white;
			}
		}

		if (countKnightLower > 0)
		{
			//knight checks
			if ((from possiblePieces in pieces
				where (possiblePieces.typeOfPiece == 10) &&
					((possiblePieces.locationOnBoard & localAttack2.knight) > 0)
				select possiblePieces.typeOfPiece).Count() > 0)
					return BoardResult.white;
		}
		
		if (countPawnLower > 0)
		{
			//check possible attack pawns
			if ((from possiblePieces in pieces
				where (possiblePieces.typeOfPiece == 9) &&
					((possiblePieces.locationOnBoard & localAttack2.pawnUpper) > 0)
				select possiblePieces.typeOfPiece).Count() > 0)
					return BoardResult.white;
		}
		return BoardResult.no;
	}
	
}
public void InitializeKings()
{
	//diagonalLeftUp;diagonalRightUp;diagonalLeftDown;diagonalRightDown;horizontalLeft;horizontalRight;verticalUp;verticalDown;knight;pawnLower;pawnUpper;
	//1
	kingAttacks.Add(1,new KingAttack(0,0,0,9241421688590303744,0,254,0,72340172838076672,132096,512,0));
	//2
	kingAttacks.Add(2,new KingAttack(0,0,256,36099303471055872,1,252,0,144680345676153344,329728,1280,0));
	//3
	kingAttacks.Add(4,new KingAttack(0,0,66048,141012904183808,3,248,0,289360691352306688,659712,2560,0));
	//4
	kingAttacks.Add(8,new KingAttack(0,0,16909312,550831656960,7,240,0,578721382704613376,1319424,5120,0));
	//5
	kingAttacks.Add(16,new KingAttack(0,0,4328785920,2151686144,15,224,0,1157442765409226752,2638848,10240,0));
	//6
	kingAttacks.Add(32,new KingAttack(0,0,1108169199616,8404992,31,192,0,2314885530818453504,5277696,20480,0));
	//7
	kingAttacks.Add(64,new KingAttack(0,0,283691315109888,32768,63,128,0,4629771061636907008,10489856,40960,0));
	//8
	kingAttacks.Add(128,new KingAttack(0,0,72624976668147712,0,127,0,0,9259542123273814016,4202496,16384,0));
	//9
	kingAttacks.Add(256,new KingAttack(0,2,0,4620710844295151616,0,65024,1,72340172838076416,33816580,131072,2));
	//10
	kingAttacks.Add(512,new KingAttack(1,4,65536,9241421688590303232,256,64512,2,144680345676152832,84410376,327680,5));
	//11
	kingAttacks.Add(1024,new KingAttack(2,8,16908288,36099303471054848,768,63488,4,289360691352305664,168886289,655360,10));
	//12
	kingAttacks.Add(2048,new KingAttack(4,16,4328783872,141012904181760,1792,61440,8,578721382704611328,337772578,1310720,20));
	//13
	kingAttacks.Add(4096,new KingAttack(8,32,1108169195520,550831652864,3840,57344,16,1157442765409222656,675545156,2621440,40));
	//14
	kingAttacks.Add(8192,new KingAttack(16,64,283691315101696,2151677952,7936,49152,32,2314885530818445312,1351090312,5242880,80));
	//15
	kingAttacks.Add(16384,new KingAttack(32,128,72624976668131328,8388608,16128,32768,64,4629771061636890624,2685403152,10485760,160));
	//16
	kingAttacks.Add(32768,new KingAttack(64,0,145249953336262656,0,32512,0,128,9259542123273781248,1075839008,4194304,64));
	//17
	kingAttacks.Add(65536,new KingAttack(0,516,0,2310355422147510272,0,16646144,257,72340172838010880,8657044482,33554432,512));
	//18
	kingAttacks.Add(131072,new KingAttack(256,1032,16777216,4620710844295020544,65536,16515072,514,144680345676021760,21609056261,83886080,1280));
	//19
	kingAttacks.Add(262144,new KingAttack(513,2064,4328521728,9241421688590041088,196608,16252928,1028,289360691352043520,43234889994,167772160,2560));
	//20
	kingAttacks.Add(524288,new KingAttack(1026,4128,1108168671232,36099303470530560,458752,15728640,2056,578721382704087040,86469779988,335544320,5120));
	//21
	kingAttacks.Add(1048576,new KingAttack(2052,8256,283691314053120,141012903133184,983040,14680064,4112,1157442765408174080,172939559976,671088640,10240));
	//22
	kingAttacks.Add(2097152,new KingAttack(4104,16512,72624976666034176,550829555712,2031616,12582912,8224,2314885530816348160,345879119952,1342177280,20480));
	//23
	kingAttacks.Add(4194304,new KingAttack(8208,32768,145249953332068352,2147483648,4128768,8388608,16448,4629771061632696320,687463207072,2684354560,40960));
	//24
	kingAttacks.Add(8388608,new KingAttack(16416,0,290499906664136704,0,8323072,0,32896,9259542123265392640,275414786112,1073741824,16384));
	//25
	kingAttacks.Add(16777216,new KingAttack(0,132104,0,1155177711056977920,0,4261412864,65793,72340172821233664,2216203387392,8589934592,131072));
	//26
	kingAttacks.Add(33554432,new KingAttack(65536,264208,4294967296,2310355422113955840,16777216,4227858432,131586,144680345642467328,5531918402816,21474836480,327680));
	//27
	kingAttacks.Add(67108864,new KingAttack(131328,528416,1108101562368,4620710844227911680,50331648,4160749568,263172,289360691284934656,11068131838464,42949672960,655360));
	//28
	kingAttacks.Add(134217728,new KingAttack(262657,1056832,283691179835392,9241421688455823360,117440512,4026531840,526344,578721382569869312,22136263676928,85899345920,1310720));
	//29
	kingAttacks.Add(268435456,new KingAttack(525314,2113664,72624976397598720,36099303202095104,251658240,3758096384,1052688,1157442765139738624,44272527353856,171798691840,2621440));
	//30
	kingAttacks.Add(536870912,new KingAttack(1050628,4227072,145249952795197440,141012366262272,520093696,3221225472,2105376,2314885530279477248,88545054707712,343597383680,5242880));
	//31
	kingAttacks.Add(1073741824,new KingAttack(2101256,8388608,290499905590394880,549755813888,1056964608,2147483648,4210752,4629771060558954496,175990581010432,687194767360,10485760));
	//32
	kingAttacks.Add(2147483648,new KingAttack(4202512,0,580999811180789760,0,2130706432,0,8421504,9259542121117908992,70506185244672,274877906944,4194304));
	//33
	kingAttacks.Add(4294967296,new KingAttack(0,33818640,0,577588851233521664,0,1090921693184,16843009,72340168526266368,567348067172352,2199023255552,33554432));
	//34
	kingAttacks.Add(8589934592,new KingAttack(16777216,67637280,1099511627776,1155177702467043328,4294967296,1082331758592,33686018,144680337052532736,1416171111120896,5497558138880,83886080));
	//35
	kingAttacks.Add(17179869184,new KingAttack(33619968,135274560,283673999966208,2310355404934086656,12884901888,1065151889408,67372036,289360674105065472,2833441750646784,10995116277760,167772160));
	//36
	kingAttacks.Add(34359738368,new KingAttack(67240192,270549120,72624942037860352,4620710809868173312,30064771072,1030792151040,134744072,578721348210130944,5666883501293568,21990232555520,335544320));
	//37
	kingAttacks.Add(68719476736,new KingAttack(134480385,541097984,145249884075720704,9241421619736346624,64424509440,962072674304,269488144,1157442696420261888,11333767002587136,43980465111040,671088640));
	//38
	kingAttacks.Add(137438953472,new KingAttack(268960770,1082130432,290499768151441408,36099165763141632,133143986176,824633720832,538976288,2314885392840523776,22667534005174272,87960930222080,1342177280));
	//39
	kingAttacks.Add(274877906944,new KingAttack(537921540,2147483648,580999536302882816,140737488355328,270582939648,549755813888,1077952576,4629770785681047552,45053588738670592,175921860444160,2684354560));
	//40
	kingAttacks.Add(549755813888,new KingAttack(1075843080,0,1161999072605765632,0,545460846592,0,2155905152,9259541571362095104,18049583422636032,70368744177664,1073741824));
	//41
	kingAttacks.Add(1099511627776,new KingAttack(0,8657571872,0,288793326105133056,0,279275953455104,4311810305,72339069014638592,145241105196122112,562949953421312,8589934592));
	//42
	kingAttacks.Add(2199023255552,new KingAttack(4294967296,17315143744,281474976710656,577586652210266112,1099511627776,277076930199552,8623620610,144678138029277184,362539804446949376,1407374883553280,21474836480));
	//43
	kingAttacks.Add(4398046511104,new KingAttack(8606711808,34630287488,72620543991349248,1155173304420532224,3298534883328,272678883688448,17247241220,289356276058554368,725361088165576704,2814749767106560,42949672960));
	//44
	kingAttacks.Add(8796093022208,new KingAttack(17213489152,69260574720,145241087982698496,2310346608841064448,7696581394432,263882790666240,34494482440,578712552117108736,1450722176331153408,5629499534213120,85899345920));
	//45
	kingAttacks.Add(17592186044416,new KingAttack(34426978560,138521083904,290482175965396992,4620693217682128896,16492674416640,246290604621824,68988964880,1157425104234217472,2901444352662306816,11258999068426240,171798691840));
	//46
	kingAttacks.Add(35184372088832,new KingAttack(68853957121,277025390592,580964351930793984,9241386435364257792,34084860461056,211106232532992,137977929760,2314850208468434944,5802888705324613632,22517998136852480,343597383680));
	//47
	kingAttacks.Add(70368744177664,new KingAttack(137707914242,549755813888,1161928703861587968,36028797018963968,69269232549888,140737488355328,275955859520,4629700416936869888,11533718717099671552,45035996273704960,687194767360));
	//48
	kingAttacks.Add(140737488355328,new KingAttack(275415828484,0,2323857407723175936,0,139637976727552,0,551911719040,9259400833873739776,4620693356194824192,18014398509481984,274877906944));
	//49
	kingAttacks.Add(281474976710656,new KingAttack(0,2216338399296,0,144115188075855872,0,71494644084506624,1103823438081,72057594037927936,288234782788157440,144115188075855872,2199023255552));
	//50
	kingAttacks.Add(562949953421312,new KingAttack(1099511627776,4432676798592,72057594037927936,288230376151711744,281474976710656,70931694131085312,2207646876162,144115188075855872,576469569871282176,360287970189639680,5497558138880));
	//51
	kingAttacks.Add(1125899906842624,new KingAttack(2203318222848,8865353596928,144115188075855872,576460752303423488,844424930131968,69805794224242688,4415293752324,288230376151711744,1224997833292120064,720575940379279360,10995116277760));
	//52
	kingAttacks.Add(2251799813685248,new KingAttack(4406653222912,17730707128320,288230376151711744,1152921504606846976,1970324836974592,67553994410557440,8830587504648,576460752303423488,2449995666584240128,1441151880758558720,21990232555520));
	//53
	kingAttacks.Add(4503599627370496,new KingAttack(8813306511360,35461397479424,576460752303423488,2305843009213693952,4222124650659840,63050394783186944,17661175009296,1152921504606846976,4899991333168480256,2882303761517117440,43980465111040));
	//54
	kingAttacks.Add(9007199254740992,new KingAttack(17626613022976,70918499991552,1152921504606846976,4611686018427387904,8725724278030336,54043195528445952,35322350018592,2305843009213693952,9799982666336960512,5764607523034234880,87960930222080));
	//55
	kingAttacks.Add(18014398509481984,new KingAttack(35253226045953,140737488355328,2305843009213693952,9223372036854775808,17732923532771328,36028797018963968,70644700037184,4611686018427387904,1152939783987658752,11529215046068469760,175921860444160));
	//56
	kingAttacks.Add(36028797018963968,new KingAttack(70506452091906,0,4611686018427387904,0,35747322042253312,0,141289400074368,9223372036854775808,2305878468463689728,4611686018427387904,70368744177664));
	//57
	kingAttacks.Add(72057594037927936,new KingAttack(0,567382630219904,0,0,0,18302628885633695744,282578800148737,0,1128098930098176,0,562949953421312));
	//58
	kingAttacks.Add(144115188075855872,new KingAttack(281474976710656,1134765260439552,0,0,72057594037927936,18158513697557839872,565157600297474,0,2257297371824128,0,1407374883553280));
	//59
	kingAttacks.Add(288230376151711744,new KingAttack(564049465049088,2269530520813568,0,0,216172782113783808,17870283321406128128,1130315200594948,0,4796069720358912,0,2814749767106560));
	//60
	kingAttacks.Add(576460752303423488,new KingAttack(1128103225065472,4539061024849920,0,0,504403158265495552,17293822569102704640,2260630401189896,0,9592139440717824,0,5629499534213120));
	//61
	kingAttacks.Add(1152921504606846976,new KingAttack(2256206466908160,9078117754732544,0,0,1080863910568919040,16140901064495857664,4521260802379792,0,19184278881435648,0,11258999068426240));
	//62
	kingAttacks.Add(2305843009213693952,new KingAttack(4512412933881856,18155135997837312,0,0,2233785415175766016,13835058055282163712,9042521604759584,0,38368557762871296,0,22517998136852480));
	//63
	kingAttacks.Add(4611686018427387904,new KingAttack(9024825867763968,36028797018963968,0,0,4539628424389459968,9223372036854775808,18085043209519168,0,4679521487814656,0,45035996273704960));
	//64
	kingAttacks.Add(9223372036854775808,new KingAttack(18049651735527937,0,0,0,9151314442816847872,0,36170086419038336,0,9077567998918656,0,18014398509481984));
}