import unittest
import requests
import json
import time
from datetime import datetime

class TestProductAPI(unittest.TestCase):
    def test_create_product_one(self):
        base_url = "http://0.0.0.0:2377/product"
        headers = {'Content-Type': 'application/json'}
        product_data = {
            "name": "8TB NAS Internal Hard Drive HDD",
            "price": 1999.99,
            "isFreeCargo": True,
            "releaseDate": "2025-01-23T00:00:00"
        }
        response = requests.post(base_url, headers=headers, data=json.dumps(product_data))
        response_json = response.json()
        print("response:", response_json)
    
    def test_get_product_list(self):
        product_id="3a17be71-1498-1b60-353d-78588b18daf7"
        base_url = f"http://0.0.0.0:2377/product/{product_id}"

    def test_get_product(self):
        guid="3a17be71-1498-1b60-353d-78588b18daf7"
        base_url = f"http://0.0.0.0:2377/product/{guid}"
        response = requests.get(base_url)
        self.assertEqual(response.status_code, 200)
        response_json = response.json()
        print(response_json)

    def test_update_product(self):
        base_url = "http://0.0.0.0:2377/product"
        headers = {'Content-Type': 'application/json'}
        product_data={
            "Id": "3a17a428-0673-85a4-c93a-b124f9d2c493",
            "name": "Seagate IronWolf 8TB NAS Internal Hard Drive HDD",
            "price": 12.3,
            "isFreeCargo": True,
            "releaseDate": "2025-01-23T00:00:00"
        }
        response = requests.put(base_url, headers=headers, data=json.dumps(product_data))
        response_json = response.json()
        print("response:", response_json)

    def test_delete_product(self):
        product_id="3a17a47c-4e0f-e230-18a8-233b4115abab"
        base_url = f"http://0.0.0.0:2377/product/{product_id}"
        response = requests.delete(base_url)
        response_json = response.json()
        print("response:", response_json)

    def test_insert_gyroscope_velocity(self):
        base_url = "http://0.0.0.0:2377/gyroscope"
        headers = {'Content-Type': 'application/json'}
        timestamp=datetime.now().timestamp()
        timestamp= time.time()
        print(timestamp)
        data = {
            "database": "root.gyroscope1",
            "measurement": "x_velocity",   
            "value": 11.4,                
            "timestamp":int(timestamp),    
        }
        # response = requests.post(base_url, params=product_data)
        response = requests.post(base_url, headers=headers, data=json.dumps(data))
        response_json = response.json()
        print("response:", response_json)


    def test_create_cpq_honeywell_compensation_service(self):
        base_url = "http://0.0.0.0:2377/cpq/honeywell_ce3245"
        headers = {'Content-Type': 'application/json'}
        product_data = {
            "serviceName": "hotfix20250205",
            "serviceVersion": "1.0.2",
            "serviceDescription": "hot fix",
        }
        response = requests.post(base_url, headers=headers, data=json.dumps(product_data))
        print("Status Code:", response.status_code)
        print("Response Text:", response.text)
        # response_json = response.json()
        # print("response:", response_json)

    def test_delete_cpq_nats_microservice(self):
        service_id="3a17a47c-4e0f-e230-18a8-233b4115abab"
        base_url = f"http://0.0.0.0:2377/cpq/{service_id}"
        response = requests.delete(base_url)
        print("Status Code:", response.status_code)
        print("Response Text:", response.text)
        # response_json = response.json()
        # print("response:", response_json)

    def test_create_compensation(self):
        base_url = "http://0.0.0.0:2377/api/app/compensation"
        headers = {'Content-Type': 'application/json'}
        new_data = {
            "deviceType": "honeywell_ce493",
            "version": "v1.5.3",
            "compensationValue": 54
            }
        response = requests.post(base_url, headers=headers, data=json.dumps(new_data))
        print("Status Code:", response.status_code)
        print("Response Text:", response.text)

    def create_compensation(self,new_data):
        base_url = "http://0.0.0.0:2377/api/app/compensation"
        headers = {'Content-Type': 'application/json'}
        response = requests.post(base_url, headers=headers, data=json.dumps(new_data))
        print("Status Code:", response.status_code)
        print("Response Text:", response.text)

    def test_create_many_compensation(self):
        NEW_DATA = [
            {
            "deviceType": "honeywell_ce493",
            "version": "v1.2.3",
            "compensationValue": 1
            },
            {
            "deviceType": "honeywell_ce493",
            "version": "v3.5.3",
            "compensationValue": 2
            },
            {
            "deviceType": "honeywell_ce493",
            "version": "v1.1.3",
            "compensationValue": 3
            },
            {
            "deviceType": "honeywell_ce493",
            "version": "v0.5.3",
            "compensationValue": 4
            },
            {
            "deviceType": "honeywell_ce493",
            "version": "v1.8.3",
            "compensationValue": 5
            },]
        for data in NEW_DATA:
            self.create_compensation(data)
            
    def create_cpq(self,new_data):
            base_url = "http://0.0.0.0:2377/api/app/contextural-physical-quality"
            headers = {'Content-Type': 'application/json'}
            response = requests.post(base_url, headers=headers, data=json.dumps(new_data))
            print("Status Code:", response.status_code)
            print("Response Text:", response.text)

    def test_create_many_contextural_physical_qualities(self):
        NEW_DATA = [
            {
        "sensorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "process": "Percentage"
        },
        {
        "sensorId": "8fa85f64-4562-4562-b3fc-2c963f66afa6",
        "process": "FahrenheitToCelsius"
        },
        {
        "sensorId": "d1c2c9fb-59f4-4b02-8c39-680b212a73e2",
        "process": "CelsiusToFahrenheit"
        },
        {
        "sensorId": "cd69ccf2-4f99-42bc-b5ad-281b8b3fdb61",
        "process": "HoneywellCompensation"
        },
        {
        "sensorId": "62a37b23-3fd9-4042-88f2-5f8251a36f80",
        "process": "SonyCompensation"
        },
        {
        "sensorId": "6d290cc1-d2b1-43ba-a62c-b4a1fa9e34f2",
        "process": "HoneywellCompensation"
        },
        ]
        for data in NEW_DATA:
            self.create_cpq(data)

if __name__ == '__main__':
    unittest.main()