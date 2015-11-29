import urllib
import json
import urllib.error

try:
    ###Login
    data = { "Login" : "Alex", "Password":"123456" }
    data = json.dumps(data) #urllib.parse.urlencode(data)
    print(data)
    data = data.encode('utf-8')
    req = urllib.request.Request("http://localhost:50956/api/user/login", data)
    req.add_header("Content-Type", "application/json");
    with urllib.request.urlopen(req) as response:
        the_page = response.read()
        auth = the_page.decode('utf-8')
    print(auth)

    ###GetUserInfo
    req = urllib.request.Request("http://localhost:50956/api/user/GetUserInfo")
    req.add_header("Authorization", auth)
    resp = urllib.request.urlopen(req)
    print(resp.read().decode("UTF-8"))

    ###GetUserInfoById?userId = N
    req = urllib.request.Request("http://localhost:50956/api/user/GetUserInfoById?userId=0")
    req.add_header("Authorization", auth)
    resp = urllib.request.urlopen(req)
    print(resp.read().decode("UTF-8"))

    ###GetOnlineUsers
    req = urllib.request.Request("http://localhost:50956/api/user/GetOnlineUsers")
    req.add_header("Authorization", auth)
    resp = urllib.request.urlopen(req)
    print(resp.read().decode("UTF-8"))
except urllib.error.HTTPError as ex:
    print(ex.getcode())
    print(ex.msg)
    print(ex.read())
except urllib.error.URLError as ex:
    print(ex.reason)
