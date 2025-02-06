

curl -X GET "http://0.0.0.0:2377/sensor?message=your_message_here"
curl -X GET "http://0.0.0.0:2377/product/index"
curl -X GET "http://0.0.0.0:2377/product/details/123e4567-e89b-12d3-a456-426614174000"
curl -X GET "http://0.0.0.0:2377/sms?message=your_message_here"

python3 -m unittest -v unittest_service.TestProductAPI.test_create_product_one
python3 -m unittest -v unittest_service.TestProductAPI.test_get_product
python3 -m unittest -v unittest_service.TestProductAPI.test_update_product
python3 -m unittest -v unittest_service.TestProductAPI.test_delete_product

python3 -m unittest -v unittest_service.TestProductAPI.test_insert_gyroscope_velocity
python3 -m unittest -v unittest_service.TestProductAPI.test_create_cpq_honeywell_compensation_service
python3 -m unittest -v unittest_service.TestProductAPI.test_delete_cpq_nats_microservice

python3 -m unittest -v unittest_service