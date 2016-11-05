#include <bits/stdc++.h>

using namespace std;

int dp[10000];
int mod = 10000007;

int doit( int n ) {
    if( n < 0 )  return 0;
    
    if(dp[n]!=-1)
        return dp[n];
    
    int ans = doit(n-1) + doit(n-2) + doit(n-3) + doit(n-4) + doit(n-5) + doit(n-6);
    ans %= mod;
    dp[n]=ans;
    
    return ans;
}

void solve(){
    int n;
    for(int i=0;i<=5;i++)
        scanf("%d",&dp[i]),dp[i]%=mod;
    
    scanf("%d",&n);
    
    for(int i=6;i<=n;i++)
        dp[i]=-1;

    printf("%d",doit(n));
    
    return ;
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