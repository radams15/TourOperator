pipeline {
    agent any

    environment {
        CONTAINER_REGISTRY = 'https://quay.io'
    }
    
    stages {
        /*stage('SAST') {
            environment {
                SEMGREP_APP_TOKEN = credentials('semgrep')
            }
            agent {
                docker {
                    image 'docker.io/returntocorp/semgrep:latest'
                    args '-u root'
                }
            }
            steps {
                script {
                    sh 'semgrep ci'
                }
            }
        }

        stage('SCA') {
            environment {
                API_KEY = credentials('nvd')
            }
            steps {
                dependencyCheck additionalArguments: '-o ./ -s ./ --prettyPrint -f ALL --nvdApiKey ${API_KEY}', odcInstallation: 'OWASP Dependency-Check'
                dependencyCheckPublisher pattern: 'dependency-check-report.xml'
            }
        }
    
        stage('Build Program') {
            steps {
                script {
                    dockerImage = docker.build "radams15/touroperator:${env.BUILD_ID}"
                }
            }
        }

        stage('Push Image') {
            steps {
                script {
                    docker.withRegistry(CONTAINER_REGISTRY, 'quay.io') {
                        dockerImage.push()
                        dockerImage.push('latest')
                    }
                }
            }
        }

        stage('DAST') {
            agent {
                docker {
                    image 'docker.io/aquasec/trivy:latest'
                    args '--entrypoint "" -u root'
                }
            }
            steps {
                sh "trivy image quay.io/radams15/touroperator:${env.BUILD_ID}"
            }
        }*/

        stage('Deploy') {
            steps {
              git branch: 'master', url: 'https://github.com/radams15/TourOperator_Playbook.git'
            
              // install galaxy roles
              sh "ansible-galaxy install -vvv -r requirements.yml"       

              ansiblePlaybook {
                  colorized: true,
                  credentialsId: 'touroperator_ssh',
                  installation: 'ansible',
                  inventory: '10.0.0.21,',
                  playbook: 'provision.yaml',
              }
            }
        }
    }

  post {
    success {
      jabberNotify buildToChatNotifier: [$class: 'PrintFailingTestsBuildToChatNotifier'], targets: 'admin@xmpp.therhys.co.uk'
    }
  }
}
