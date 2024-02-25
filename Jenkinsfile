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
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:7.0'
                }
            }
            steps {
                sh 'dotnet restore'
                sh 'dotnet publish -c Release -o out'
                sh 'dotnet publish --os linux --arch x64 /p:PublishProfile=DefaultContainer -c Release'
                
                script {
                    archiveArtifacts artifacts: 'out/'
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
