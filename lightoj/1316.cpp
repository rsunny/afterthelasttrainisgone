#include <bits/stdc++.h>

using namespace std;

#define MAXN (1<<16)+9

const int INF = 100000000;

typedef pair<int,int> PII;

map<int,int> dist[509];
int adj[17][17];
vector<int> edges[509],wt[509];
int S,n,m,u,v,w,mask,temp;
int vis[509];
int mark[509];
priority_queue<pair<int,PII>, vector<pair<int,PII> >, greater<pair<int,PII> > > Q;	

void dij(){  
  
  Q.push (make_pair (0, make_pair(mask,u)));

  dist[0][mask] = 0;

  while (!Q.empty()){
    pair<int,PII> p = Q.top();
    Q.pop();

    u = p.second.second;
    temp = mask = p.second.first;

    if(p.first > dist[u][mask])
		continue;

	for(int i=0;i<(int)edges[u].size();i++){
		v = edges[u][i];
		w = wt[u][i];
		if(dist[u][mask] + w < dist[v][mask]){
			dist[v][mask|(1<<vis[v])] = dist[u][mask] + w;
			dist[v][mask] = dist[v][mask|(1<<vis[v])] ;
			mask |= |(1<<vis[v]);
			Q.push(make_pair(dist[v][mask],make_pair(mask,v))); 
		}
		mask=temp;
	}
  }
  
  return ;
}


void solve(){
	scanf("%d %d %d",&n,&m,&S);

	for(int i=0;i<n;i++){
		edges[i].clear();
		wt[i].clear();
		dist[i][0]=INF;
		mark[i]=0;
	}

	while( Q.size())
		Q.pop();

	int cnt=0;

	for(int i=0;i<S;i++){
		scanf("%d",&u);
		mark[u]=cnt++;
	}
	
	for(int i=0;i<m;i++){
		scanf("%d %d %d",&u,&v,&w);
		edges[u].push_back(v);
		wt[u].push_back(w);
	}
	
	for(int i=0;i<n;i++){
		if(mark[i]){
			dij();
			for(int j=0;j<n;j++){
				if(mark[j])
					adj[mark[i]][mark[j]]=dist[j][0];
				dist[j][0]=INF;
			}
		}
	}

	int max_cnt=0;
	int min_dist=INF;
	u = n-1;

	map<int,map<int,bool> > :: iterator it;

	for(it=here.begin();it!=here.end();it++){
		int i=it->first;
		cnt = __builtin_popcount (i);
		if(cnt>max_cnt and here[i][u]){
			max_cnt=cnt;
			min_dist=dist[i][u];
		}
		else if(cnt==max_cnt and here[i][u]){
			max_cnt=cnt;
			min_dist=min(min_dist,dist[i][u]);
		}
	}

	if(min_dist==INF){
		printf("Impossible");
	}
	else{
		printf("%d %d", max_cnt,min_dist);
	}
	
	return;
}

int main(){
	int TEST;
	cin>>TEST;
	for(int test=1;test<=TEST;test++){
		printf("Case %d: ",test);
		solve();
		printf("\n");
	}
	return 0;
}