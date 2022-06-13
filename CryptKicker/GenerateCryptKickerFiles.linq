<Query Kind="Program" />

void Main()
{
	string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\testfile.txt";
	string answerFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\answerfile.txt";
	string badSubstitution ="xxxxxxxxxxxxxxxxxxxx";
	Random r = new Random();
	int numberOfHands = 1000;
	char[] letters = new char[26] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
	string[] words = new string[551] {"abnormal","above","aboveground","absent","absolution","absorbable","abstractly","accidental","accomplice","accountable","ache","acid","adaptive","addictive","adult","adventure","aerobatic","affair","agitator","aim","airship","airtight","alien","allow","alternate","aluminium","ambivalent","american","ammunition","amongst","amplitude","angriest","annoying","anteater","anxiety","anywhere","apparatus","arms","arrival","arrow","arrows","arson","art","artificial","atmosphere","attic","audacity","authority","aviator","awful","back","backyard","bad","balance","ballistic","barbershop","barnyard","base","bases","battle","battleground","beard","beauty","becoming","bedtime","beefcake","begging","behind","believe","believer","belong","belongings","bend","bestial","better","beyond","bible","biggest","bike","binocular","bionic","black","blasted","bleakly","bleakness","bleeding","blend","blimp","blindly","blizzard","bloat","bloodsport","bloody","blueprint","bluish","blunder","blunt","boast","body","booze","born","boulevard","bounce","bovine","bowyer","box","brainwasher","brave","brigade","bright","brimstone","bring","bronze","brush","brutal","bubble","bughouse","building","bulletin","bully","bumble","burnt","calculation","calibration","calling","candy","captivity","captured","carbon","carnal","casino","catch","center","channel","chapter","chieftain","choker","chromatic","chromosome","circle","circuit","cloth","coal","coat","coffin","cognitive","cola","collarbone","collection","collision","colony","column","comatose","companion","compound","comrade","condo","confusion","conqueror","contest","contestant","contortionist","contrast","conversion","corporation","cosmonaut","costume","costumed","cotton","cough","courage","courageous","coward","crack","crasher","crater","creature","cricket","crunch","cry","crypt","cuddly","curved","cut","cuteness","damnation","dangerous","data","daydreamer","days","deathtrap","debate","debug","decadent","deceit","decipherer","decode","defector","definitive","deformity","deliverance","design","destruction","detachable","details","devil","diamond","different","disaster","disintegration","disk","dismember","distortion","diversion","divinity","doberman","document","dogtooth","doll","domesticated","dominant","donkey","dope","down","downtown","drag","dramatic","dreamless","drench","driver","droplet","droppings","drowned","drugstore","dual","duel","dynasty","eat","edge","ego","egocentric","elastic","elevation","emotional","empire","empty","enemies","energy","enlighten","enrage","equation","erotica","excess","executioner","exile","existing","exit","exorcism","export","expressive","extravagant","faction","fail","fake","falling","falls","fanatic","fanatical","farm","fat","fatal","felon","felt","ferment","fermentation","festival","field","fiendish","fighting","finch","finger","fingertip","finite","fix","flaming","flatness","flatten","flight","flood","flowers","fly","flytrap","fog","foggy","fold","foot","forger","forgotten","fork","form","fortuneteller","frame","frantic","freewill","french","frequent","frozen","fundamental","fuse","fuzz","garage","gargantuan","genetic","ghetto","gifted","glamorous","glitter","gloomy","goon","gradient","grainy","graphic","grey","grinning","grip","gritty","group","growl","grunting","guaranteed","guilt","gum","habitual","hammerhead","hangover","happy","harsh","heal","healer","healthy","heartsick","heaven","heaviest","heavyset","helmet","historic","hoaxer","hobby","holiest","home","homesick","honeybee","honor","hop","horrible","horseplay","horses","hose","hospital","humanlike","hunchback","hundred","hungry","hurdle","hype","identical","identity","ignorant","imposter","improper","ink","island","jackknife","jerid","junior","justice","juvenile","kind","king","kitten","large","leather","legendary","level","lime","liquid","liquor","lockbox","locusca","lollipop","loneliness","long","lurker","lustre","magnetic","marble","marginal","mask","melt","memory","metal","mirror","mixer","mobster","modern","mohawk","molten","moment","momentary","mongrel","mood","morbid","mouth","muscular","mutagen","mutilation","naive","necrotic","negative","nerve","nightmare","norm","nuclear","nude","numbskull","ocean","official","opposition","original","orphan","orphanage","owl","painkiller","pale","paper","paralysis","passenger","pearl","pelvic","peppermint","perilous","periodic","personal","pesky","phantom","phonetic","pictorial","pieces","pill","pilot","pin","pineapple","pitch","plant","poison","polar","port","portrait","powerless","prank","privilege","production","prong","proof","proper","provider","punch","puppet","pyramids","rapid","rasse","rat","reason","red","regional","reptile","republic","revenge","revolt","rich","robotic","ruby","rum","sabotage","sadistic","sadness","sand","sauce","savage","scanner","scar","scheme","schemer","sector","series","serum","servant","seven","sex","sexual","shag","shaman","shameful","sideshow","simple","skyline","slap","sleep","small","smoke","smuggler","some","southern","spider","spirits","sprite","stiff","suckle","sun","surgeon","symbol","symbolic","tense","thief","threat","tooth","total","toy","trauma","triangle","truth","ugly","unliving","uptown","useless","vacant","viper","virgin","volcanic","vulture","wake","war","warning","weak","whale","whisper","wish","wonder","world","zebra","zero"};
	
	StringBuilder results = new StringBuilder();
	StringBuilder answers = new StringBuilder();
	results.AppendLine("551");
	foreach (var element in words)
	{
		results.AppendLine(element);
	}
	
	for (int i = 0; i < numberOfHands; i++)
	{	
		bool useBad = false;
		
		if (r.Next(1, 10) == 4)
			useBad = true;
			
		Dictionary<char, char> translation = GenerateFullTranslation(useBad);
		
		string line = string.Empty;
		string answer = string.Empty;
		foreach (var element in Enumerable.Range(0,5510).OrderBy(k => Guid.NewGuid()).Take(20)) 
		{
			string originalString = words[element % 551];
			string translatedString = string.Empty;
			for (int j = 0; j < originalString.Length; j++)
			{
				translatedString += translation[originalString[j]];
			}
			
			if (line == string.Empty)
			{
				line = translatedString;
				
				if (useBad)
					answer = badSubstitution.Substring(0, originalString.Length);
				else
					answer = originalString;
			}
			else if (line.Length + 1 + translatedString.Length < 80)
			{	
				line = line + " " + translatedString;
				
				if (useBad)
					answer = answer + " " + badSubstitution.Substring(0, originalString.Length);
				else
					answer = answer + " " + originalString;
			}
		}
//		answer.Dump();
//		line.Dump();
//		translation.Dump();
		results.AppendLine(line);	
		answers.AppendLine(answer);
	}
	System.IO.File.WriteAllText(outputFile, results.ToString());
	System.IO.File.WriteAllText(answerFile, answers.ToString());
}

// Define other methods and classes here
public Dictionary<char, char> GenerateFullTranslation(bool useBad)
{
	Dictionary<char, char> translation = new Dictionary<char,char>();
	int letterCount = 0;
	if (useBad)
	{
		char[] letters = new char[26] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
		char[] letters2 = new char[26] {'a','a','t','t','e','s','s','s','s','c','c','t','d','d','n','e','e','r','s','a','i','o','n','n','n','n'};
		foreach (var element in Enumerable.Range(0,26).OrderBy(k => Guid.NewGuid()).Take(26)) // range starts with 0, so subtract one from the number of names, randomize, then take the number of canidates
		{
			translation.Add(letters[letterCount++], letters2[element]);
		}
	}
	else
	{
		char[] letters = new char[26] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};

		foreach (var element in Enumerable.Range(0,26).OrderBy(k => Guid.NewGuid()).Take(26)) // range starts with 0, so subtract one from the number of names, randomize, then take the number of canidates
		{
			translation.Add(letters[letterCount++], letters[element]);
		}
	}
	return translation;
}