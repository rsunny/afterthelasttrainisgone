#include <bits/stdc++.h>

using namespace std;

int readInt () {
	bool minus = false;
	int result = 0;
	char ch;
	ch = getchar();
	while (true) {
		if (ch == '-') break;
		if (ch >= '0' && ch <= '9') break;
		ch = getchar();
	}
	if (ch == '-') minus = true; else result = ch-'0';
	while (true) {
		ch = getchar();
		if (ch < '0' || ch > '9') break;
		result = result*10 + (ch - '0');
	}
	if (minus)
		return -result;
	else
		return result;
}

int n,x,t,k;
vector<int> dim[109];
int cost[109];
int fav[109];
int tot_mny;
int tot_buy;

//dp[pos][mny_used][num_buy]
int dp[101][1101][23];

bool check(int used){
	int tmp=used;
	used+=(tmp/10);
	if(tmp%10)
		used++;
	return used>tot_mny;
}

int doit(int pos,int used,int buy){
	if(check(used) or buy>tot_buy)
		return -1000000;
	
	if(pos==k)
		return 0;

	if(dp[pos][used][buy]!=-1)
		return dp[pos][used][buy];
	
	int res=0;
	for(int i=0;i<3;i++){
		int temp_res=doit(pos+1,used+(i*cost[pos]),buy+i);
		res=max(res,temp_res+i*fav[pos]);
	}
	dp[pos][used][buy]=res;
	return res;
}

int main(){
	int res;
	while(1){
		n = readInt();
		x = readInt();
		t = readInt();
		k = readInt();
		
		if(n==0 and x==0 and t==0 and k==0)
			return 0 ;
		
		for(int i=0;i<k;i++){
			int tot=0;
			int val;
			for(int j=0;j<=n+1;j++){
				val = readInt();
				if(j==0)
					cost[i]=val;
				else
					tot+=val;
			}
			fav[i]=tot;
		}
		tot_mny=x*(n+1);
		tot_buy=2*(n+1);

		for(int i=0;i<k;i++)
			for(int j=0;j<=tot_mny;j++)
				for(int k=0;k<=tot_buy;k++)
					dp[i][j][k]=-1;

		res=doit(0,t*(n+1),0);
		float prt=res;
		prt/=(float)(n+1);
		printf("%.2f\n",prt);
	}
	return 0;
}