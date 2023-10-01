FILES=$(find ./  -type f -name '*.proto' -exec basename {} \; | awk -F'[_.]' '{print "/api/"$1".proto "}')

echo $FILES

rm `pwd`/generated/*

docker run \
    -v `pwd`/Protos:/api \
    -v `pwd`/generated:/out \
	-u="1000" \
	--name="grpc-web-generator" \
	--rm \
    jfbrandhorst/grpc-web-generators \
    protoc -I /api \
		--js_out=import_style=commonjs:/out \
        --grpc-web_out=import_style=typescript,mode=grpcwebtext:/out \
        $FILES
        	