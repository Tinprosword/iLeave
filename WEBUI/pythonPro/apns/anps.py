import asyncio
from uuid import uuid4
from aioapns import APNs, NotificationRequest, PushType


async def run():
    
    apns_key_client = APNs(
        key='../apns/apns-key.p8',
        key_id='A533U9KJ66',
        team_id='84Z2AMFMF3',
        topic='com.mr-noone.apns-tool',  # Bundle ID
        use_sandbox=False,
    )
    request = NotificationRequest(
        device_token='0EA5869282D55F4EFCC1B172D40DF9A42B48DECC61E6E9275260A25CA63FF9F7',
        message = {
            "aps": {
                "alert": "Hello from APNs",
                "badge": "1",
            }
        },
        notification_id=str(uuid4()),  # optional
        time_to_live=3,                # optional
        push_type=PushType.ALERT,      # optional
    )
    #await apns_cert_client.send_notification(request)
    await apns_key_client.send_notification(request)

loop = asyncio.get_event_loop()
loop.run_until_complete(run())