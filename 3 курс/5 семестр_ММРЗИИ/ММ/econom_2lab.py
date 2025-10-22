def readCostMatrix(filename):
    with open (filename, 'r') as f:
        matrix=[]
        for line in f:
            matrix.append(list(map(int, line.strip().split())))
        return matrix
    
def findMinCost(costMatrix, assignments, row, currentCost):
    n=len(costMatrix)
    if row==n:
        return currentCost
    minCost=float('inf')
    for col in range(n):
        if col not in assignments:
            assignments.add(col)
            newCost=findMinCost(costMatrix,assignments,row+1,currentCost+costMatrix[row][col])
            minCost=min(minCost,newCost)
            assignments.remove(col)
    return minCost


filename = input ("Введите название файла: ")
costMatrix = readCostMatrix(filename)

minCost=findMinCost(costMatrix, set(), 0,0)

print(f"Минимальная стоимость назначения: {minCost}")
