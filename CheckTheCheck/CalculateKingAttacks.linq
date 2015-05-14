<Query Kind="Program" />

void Main()
{
	int row = 1;
	int column = 1;
	
	for (int i = 1; i <= 64; i++)
	{
		int j, k;
		
		ulong diagonalLeftUp = 0;
		k = column;
		for (j = row - 1; j >= 0; j--)
			diagonalLeftUp +=CalculateBit(--k,j);

		ulong diagonalRightUp  = 0;
		k = column;
		for (j = row - 1; j > 0; j--)
			diagonalRightUp += CalculateBit(++k, j);
		
		ulong diagonalLeftDown = 0;
		k = column;
		for(j = row + 1; j <= 8; j++)
			diagonalLeftDown += CalculateBit(--k,j);
		
		ulong diagonalRightDown = 0;
		k = column;
		for(j = row + 1; j <= 8; j++)
			diagonalRightDown += CalculateBit(++k, j);
		
		ulong horizontalLeft = 0;
		for (j = 0; j < column; j++)
			horizontalLeft += CalculateBit(j, row);
		
		ulong horizontalRight = 0;
		for (j = 8; j > column; j--)
			horizontalRight += CalculateBit(j, row);
		
	
		ulong verticalUp = 0;
		for (j = 0; j < row; j++)
			verticalUp += CalculateBit(column, j);

		ulong verticalDown = 0;
		for (j = 8; j > row; j--)
			verticalDown += CalculateBit(column, j);
			
		ulong knight = 0;
		knight += CalculateBit(column + 1, row + 2);
		knight += CalculateBit(column + 2, row + 1);
		knight += CalculateBit(column - 1, row + 2);
		knight += CalculateBit(column - 2, row + 1);
		knight += CalculateBit(column + 1, row - 2);
		knight += CalculateBit(column + 2, row - 1);
		knight += CalculateBit(column - 1, row - 2);
		knight += CalculateBit(column - 2, row - 1);
		
		ulong pawnLower = 0;
		pawnLower += CalculateBit(column - 1, row + 1);
		pawnLower += CalculateBit(column + 1, row + 1);
		
		ulong pawnUpper = 0;
		pawnUpper += CalculateBit(column - 1, row - 1);
		pawnUpper += CalculateBit(column + 1, row - 1);
	
		string output = string.Format("//{0}\nkingAttacks.Add({1},new KingAttack({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}));",i,CalculateBit(column,row),
																				diagonalLeftUp, diagonalRightUp, diagonalLeftDown, diagonalRightDown,
																				horizontalLeft, horizontalRight,
																				verticalUp, verticalDown,
																				knight, pawnLower, pawnUpper);
		output.Dump();
		
		//set for next
		if(++column == 9)
		{
			++row;
			column = 1;
		}
	}
}

// Define other methods and classes here
public ulong CalculateBit(int column, int row)
{
	//if anything is outside our grid, return zero
	if (column < 1 || row < 1 || column > 8 || row > 8)
		return (ulong)0;
	return ((ulong)1 << ((column - 1) + ((row - 1) * 8)));
	//return ((ulong)Math.Pow(2,((column - 1) + ((row - 1) * 8))));
}