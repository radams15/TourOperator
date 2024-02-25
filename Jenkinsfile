pipeline {
    agent any

    environment {
        CONTAINER_REGISTRY = 'registry.redhat.io'
    }
    
    stages {
        /*stage('SAST') {
            environment {
                SEMGREP_APP_TOKEN = credentials('semgrep')
            }
            agent {
                docker {
                    image 'docker.io/returntocorp/semgrep:latest'
                }
            }
            steps {
                script {
                    sh 'semgrep ci'
                }
            }
        }*/

        /*stage('SCA') {
            environment {
                API_KEY = credentials('nvd')
            }
            steps {
                dependencyCheck additionalArguments: '-o ./ -s ./ --prettyPrint -f ALL --nvdApiKey ${API_KEY}', odcInstallation: 'OWASP Dependency-Check'
                dependencyCheckPublisher pattern: 'dependency-check-report.xml'
            }
        }*/
    
        stage('Build Program') {
            steps {
                script {
                    dockerImage = docker.build "radams15/touroperator:${env.BUILD_ID}"
                }
            }
        }

        stage('Push Image') {
            environment {
                REGISTRY_CREDS = credentials('redhat-registry')
            }

            steps {
                script {
                    docker.withRegistry(CONTAINER_REGISTRY, REGISTRY_CREDS)
                    dockerImage.push()
                    dockerImage.push('latest')
                }
            }
        }

        stage('DAST') {
            agent {
                docker {
                    image 'docker.io/aquasec/trivy'
                    args '--entrypoint='
                }
            }
            steps {
                sh 'trivy image docker.io/debian:10'
            }
        }
    }
}
