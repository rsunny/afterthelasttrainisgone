#include <bits/stdc++.h>

using namespace std;

#define NIL -1

vector<pair<int,int> > res;
bool visited[100009];
int disc[100009];
int low[100009];
int parent[100009];

class Graph{
	int V; 
	vector<int> adj[100009];
	void bridgeUtil(int v);
public:
	Graph(int V);
	void addEdge(int v, int w);
	void bridge();
};

Graph::Graph(int V){
	this->V = V;
}

void Graph::addEdge(int v, int w){
	adj[v].push_back(w);
	adj[w].push_back(v);
	return ;
}

void Graph::bridgeUtil(int u){
	static int time = 0;

	visited[u] = true;

	disc[u] = low[u] = ++time;

	for (int i = 0; i < (int)adj[u].size(); ++i){
		int v = adj[u][i];
		if (!visited[v]){
			parent[v] = u;
			bridgeUtil(v);
			low[u] = min(low[u], low[v]);
			if (low[v] > disc[u])
				res.push_back(make_pair(min(u,v),max(u,v)));

		}

		else if (v != parent[u])
			low[u] = min(low[u], disc[v]);
	}

	return ;
}

void Graph::bridge(){

	for (int i = 0; i < V; i++){
		parent[i] = NIL;
		visited[i] = false;
	}

	for (int i = 0; i < V; i++)
		if (visited[i] == false)
			bridgeUtil(i);

	for(int i=0;i<V;i++)
		adj[i].clear();

	return ;	
}

void solve(int test){
	int n;
	scanf("%d",&n);
	Graph g(n+9);
	for(int i=0;i<n;i++){
		int u,e,v;
		scanf("%d (%d)",&u,&e);
		for(int j=0;j<e;j++){
			scanf("%d",&v);
			g.addEdge(u,v);
		}
	}
	res.clear();
	g.bridge();
	sort(res.begin(), res.end());
	printf("Case %d:\n",test);
	printf("%d critical links\n", (int)res.size());
	for(int i=0;i<(int)res.size();i++)
		printf("%d - %d\n", res[i].first,res[i].second);
	return;
}

int main(){
	int TEST;
	scanf("%d",&TEST);
	for(int test=1;test<=TEST;test++){
		solve(test);
	}
	return 0;
}

