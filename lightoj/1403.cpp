#include <bits/stdc++.h>

using namespace std;

#define MAXN 1009

int n,m;

vector<int> g[MAXN];
int visited[MAXN];
int pairs[MAXN];

bool bpm(int v) {
  if (visited[v]) 
    return false;
  visited[v] = true;
  for (size_t i = 0; i<g[v].size(); i++) {
    if (pairs[g[v][i]] == -1 || bpm(pairs[g[v][i]])) {
      pairs[g[v][i]] = v;
      return true;
    }
  }
  return false;
}

void solve(int test) {
  scanf("%d %d", &n, &m);
  int a,b;
  for (int i = 0; i<m; i++) {
    scanf("%d %d", &a, &b);
    a--,b--;
    g[a].push_back(b);
  }
  
  for(int i=0;i<n;i++)
    pairs[i]=-1;

  int cnt=0;

  for (int i = 0; i<n; i++) {
    memset(visited, 0, sizeof(visited));
    if(bpm(i))
      cnt++;
  }

  printf("Case %d: %d\n",test,n-cnt);
  
  for(int i=0;i<n;i++){
    g[i].clear();
  }

  return ;
}

int main(){
  int TEST;
  cin>>TEST;
  for(int test=1;test<=TEST;test++){
    solve(test);
  }
  return 0;
}
