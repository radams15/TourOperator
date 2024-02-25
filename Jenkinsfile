pipeline {
    agent any
    
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
                    dockerImage = docker.build 'touroperator'
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
