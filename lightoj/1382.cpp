#include <bits/stdc++.h>

using namespace std;

#define MAXN 1005

long long int mod = 1000000007;

vector<int> ed[MAXN];
long long int c[MAXN][MAXN];
int subtree[MAXN];
int vis[MAXN];
int n;

void pre(){
	for(int i=0;i<MAXN;i++){
		c[i][0]=c[i][i]=1;
		for(int j=1;j<i;j++){
			c[i][j]=c[i-1][j]+c[i-1][j-1];
			c[i][j]%=mod;
		}
	}
	return;
}

long long int doit(int u){
	long long int res = 1;
	int tot=0,v;

	for(int i=0;i<(int)ed[u].size();i++){
		v=ed[u][i];
		res*=doit(v); res%=mod;
		tot+=subtree[v];
	}

	subtree[u]=tot+1;

	for(int i=0;i<(int)ed[u].size();i++){
		v=ed[u][i];v=subtree[v];
		res*=c[tot][v]; res%=mod;
		tot-=v;
	}
	return res;
}

void solve(){
	int u,v;
	scanf("%d",&n);
	for(int i=1;i<n;i++){
		scanf("%d %d",&u,&v);
		vis[v]=1;
		ed[u].push_back(v);
	}
	
	for(int i=1;i<=n;i++){
		if(vis[i]==0){
			printf("%lld",doit(i));	
			break;
		}
	}

	for(int i=1;i<=n;i++){
		vis[i]=0;
		subtree[i]=0;
		ed[i].clear();
	}
	return;
}

int main(){
	pre();
	int TEST;
	cin>>TEST;
	for(int test=1;test<=TEST;test++){
		printf("Case %d: ",test);
		solve();
		printf("\n");
	}
	return 0;
}