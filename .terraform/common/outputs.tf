output "iam_profile_name" {
  value = aws_iam_instance_profile.customcads_instance_profile.name
}

output "vpc_id" {
  value = aws_vpc.customcads_vpc.id
}

output "db_subnet_group" {
  value = aws_db_subnet_group.customcads_subnet_group.name
}

output "elb_subnet_id" {
  value = aws_subnet.customcads_subnet_public2_b.id
}

output "db_security_group" {
  value = aws_security_group.customcads_db_security_group.id
}

output "eb_security_group" {
  value = aws_security_group.customcads_app_security_group.id
}

output "ec2_key_name" {
  value = aws_key_pair.customcads_key_pair.key_name
}

output "eb_service_role" {
  value = aws_iam_role.customcads_eb_service_role.arn
}

output "eb_notification_arn" {
  value = aws_sns_topic.customcads_notification.arn
}

output "cloudfront_cache_policy_id" {
  value = aws_cloudfront_cache_policy.customcads_cache_policy.id
}

output "certificate_arn" {
  value = aws_acm_certificate.customcads_certificate.arn
}
