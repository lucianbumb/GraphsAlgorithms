
using System.Threading.Channels;
using GraphsAlgorithms;


//TEMA Nr. 1. Să se alcătuiască un program care calculează Mk cu k=1, 2, ...
//şi stabileşte pentru fiecare aij0 care sunt lanţurile (drumurile)
//de lungime k între vârfurile xi şi xj.


Console.WriteLine("========== Tema nr. 1 =================");

var graph = SampleData.GetGraphModel();

graph.PrintMatrix();
graph.PrintNodes();
Console.WriteLine("========== END =================");
Console.WriteLine();


//TEMA Nr. 4. Să se întocmească un program care să parcurgă un graf prin metoda „în lăţime”


Console.WriteLine("========== Tema nr. 4 =================");
Console.WriteLine(SampleData.GraphView);
graph.PrintBreadthFirstSearch(graph.Nodes.First());

Console.WriteLine("========== END =================");
Console.WriteLine();


//Tema: TEMA Nr. 8. Să se întocmească programul care determină drumul de valoare minimă între 2 vârfuri ale unui 
//graf, cu algoritmul Bellman-Kalaba.
Console.WriteLine("========== Tema nr. 8 =================");
var bkGraph = new BellmanKalabaGraph();
bkGraph.PrintBellmanKalabaDistancePathMinValue(bkGraph.Graph.Nodes.First(),true);

Console.WriteLine("========== END =================");
Console.WriteLine();


//TEMA Nr. 9. Să se întocmească programul care determină drumul de valoare maximă între 2 vârfuri ale unui 
//graf, cu algoritmul Bellman-Kalaba.

Console.WriteLine("========== Tema nr. 9 =================");
bkGraph.PrintBellmanKalabaDistancePathMaxValue(bkGraph.Graph.Nodes.First());

Console.WriteLine("========== END =================");
Console.WriteLine();

//TEMA Nr. 14. Să se întocmească un program care pentru un graf G determină dacă graful este eulerian şi 
//dacă da, stabileşte care este un drum (lanţ) de tipul respectiv

Console.WriteLine("========== Tema nr. 14 =================");
var eGraph = new EulerianGraph();

Console.WriteLine($"Is Eulerian Graph: {eGraph.IsEulerian()}");

eGraph.PrintCircuit();
Console.WriteLine("========== END =================");
Console.WriteLine();
//TEMA Nr. 23. Să se întocmească programul care realizează planificarea optimală a unei suite de lucrări.
Console.WriteLine("========== Tema nr. 23 =================");

var buildingHouseGraph=new BuildingHouseGraph();

buildingHouseGraph.PrintHouseExecution();

Console.WriteLine("========== END =================");
Console.WriteLine();

Console.ReadKey();