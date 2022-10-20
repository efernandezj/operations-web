# Operations Web
Local dev deployment

## Requirements for local dev
* AWS command line SDK
* Docker

## Contents
* template.yaml - cloudformation template
* parameters.json - parameters for dev environment, manual deployment

## To deploy changes manually
To deploy changes in template.yaml or parameters.json


* Obtain AWS command line credentials from https://envista.awsapps.com/start#/
```
export AWS_ACCESS_KEY_ID="*******"
export AWS_SECRET_ACCESS_KEY="*******"
export AWS_SESSION_TOKEN="*******"
```
* Login Docker and get AWS cli for local dev image, and start it from current folder
```
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 028137510215.dkr.ecr.us-east-1.amazonaws.com
docker pull 028137510215.dkr.ecr.us-east-1.amazonaws.com/awscli:latest
docker run -v $(pwd):/data -it 028137510215.dkr.ecr.us-east-1.amazonaws.com/awscli:latest bash
cd data
```
* Deploy changes
```
cfn-include template.yaml > template.json
cloudformation_update_stack_template us-east-1 operations-web-dev template.json parameters.json 900 arn:aws:iam::028137510215:role/ormco_service_aws_deployer
```
* Deploy content
  * Manually copy content to S3 bucket, 
  * Take bucket name from Cloud Formation stack outputs
```
aws s3 sync --delete ./ s3://operations-web-dev-s3bucket-1w9yix73ydzx4/
```
* Invalidate Cloud Front distribution
  * Take credentials and distribution ID from Cloud Formation stack outputs
```
export AWS_ACCESS_KEY_ID="AKIAQNDJAKVDSU76FBVL"
export AWS_SECRET_ACCESS_KEY="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"
aws cloudfront create-invalidation --distribution-id E2MJ6QZWAV376L --paths '/*'
```
