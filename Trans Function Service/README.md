## cPQ Nats 服務設計
### API設計
所有NATS微服務都採 一個double做範例，之後可以自己擴增
但給SAM的就是一個double
新增調整honeywell_CE3245設定
目的，不同廠商不同裝置會有不同補償的參數，參數存在PG，微服務部屬在該服務中
在測試階段或是hotfix時，可能會用到
因為可能是每個時間點都要補償，工作量可能很大，蠻符合實際需求

請求
服務名稱，不能強制percentage，因為是牽涉如何收消息
服務版本
服務備註

功能
會去抓PG裡面該裝置的參數予以補償到原有的值
新增調整tasto_ks456設定
新增百分比服務
查詢所有服務
刪除服務

### NATS服務調用PG設計
如果把PG寫在NATS微服務中，會有NATS又依賴PG問題，應該在service層依賴PG和NATS，避免NATS和PG耦合問題

### API

Create a client mock to send request to service

1. get all service and status
GET /cpq

Response
{
  "results": [
    {
      "service_name": "20250125_trial",
      "service_version": "v1.5.3",
      "service_description": "for initial trial",
      "service_id": "42c1d4a3-f2b9-4b71-9452-d76a45939e92"
    }
  ]
}


2. create new microservice
POST /cpq/honeywell_ce3245
Content-Type: application/json
{
  "service_name": "20250205_hotfix",
  "service_version": "v1.0.0",
  "service_description": "for hotfix"
}

Response
{
  "status": "ok",
  "service_id": "f47ac10b-58cc-4372-a567-0e02b2c3d479"
}

POST /cpq/tasto_ks456
Content-Type: application/json
{
  "service_name": "20250125_trial",
  "service_version": "v1.5.3",
  "service_description": "for initial trial"
}

Response
{
  "status": "ok",
  "service_id": "42c1d4a3-f2b9-4b71-9452-d76a45939e92"
}

POST /cpq/percentage
Content-Type: application/json
{
  "service_name": "20250125_trial",
  "service_version": "v1.5.3",
  "service_description": "for initial trial"
}

Response
{
  "status": "ok",
  "service_id": "42c1d4a3-f2b9-4b71-9452-d76a45939e92"
}

3. Delete exist services
DELETE /cpq/{service_id}

Response
{
  "status": "ok"
}

Response with Error
{
  "error": "Service not found"
}

{
  "error": "Invalid service_id format"
}




