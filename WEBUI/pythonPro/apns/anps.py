import sys
import asyncio
from uuid import uuid4
from aioapns import APNs, NotificationRequest, PushType

a=eval(sys.argv[1])

async def run(title):
    
    apns_key_client = APNs(
        key='C:\\Users\\Administrator\\source\\repos\\WebIleave\\WEBUI\\pythonPro\\apns\\ileave_apns_develop.p8',
        key_id='9S2ZRZ8C2S',
        team_id='LG8S9KFR3V',
        topic='com.dwsolutions.dw-ihr',  # Bundle ID
        use_sandbox=False,
    )
    request = NotificationRequest(
        device_token='0ea0ddfb0e67e098f7545d99578ee4882033518336ed18b598703b9edf668f5e',
        message = {
            "aps": {
                "alert": a,
                "badge": "1",
            }
        },
        notification_id=str(uuid4()),  # optional
        time_to_live=3,                # optional
        push_type=PushType.BACKGROUND,      # optional ALERT backgroud
    )
    #await apns_cert_client.send_notification(request)
    await apns_key_client.send_notification(request)

loop = asyncio.get_event_loop()
loop.run_until_complete(run(a))