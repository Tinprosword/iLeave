import sys
import asyncio
import os
import pathlib
from uuid import uuid4
from aioapns import APNs, NotificationRequest, PushType

a=sys.argv[1]
b=sys.argv[2]
c=sys.argv[3]
print(c)

async def run(title,devicetoken,p8path):
	#rootPath= os.path.abspath('./')
	#print('aa')
    apns_key_client = APNs(
        key=p8path,
        key_id='9S2ZRZ8C2S',
        team_id='LG8S9KFR3V',
        topic='com.dwsolutions.dw-ihr',  # Bundle ID
        use_sandbox=False,
    )
    request = NotificationRequest(
        device_token=devicetoken,
        message = {
            "aps": {
                "alert": title,
                "badge": "1",
            }
        },
        notification_id=str(uuid4()),  # optional
        time_to_live=3,                # optional
        push_type=PushType.ALERT,      # optional ALERT BACKGROUND
    )
    #await apns_cert_client.send_notification(request)
    await apns_key_client.send_notification(request)

loop = asyncio.get_event_loop()
loop.run_until_complete(run(a,b,c))