package GUI;

import Proxy.ProxyClient;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Modality;
import javafx.stage.Stage;

/**
 * Created by andrei on 2017-04-13.
 */
public class LoginController {
    @FXML
    Button SignInButton;
    @FXML
    TextField nameTextField;
    @FXML
    PasswordField passwordTextField;
    private ProxyClient userController;
    private ContestController contestGUIController;
    private FXMLLoader contestLoader;
    private Stage stage;
    private Parent scene;
    private Stage mainStage;

    public LoginController() {

    }

    public void initialiseComponents(ProxyClient userController,
                                     Stage mainStage) throws Exception {
        this.userController = userController;
        this.mainStage = mainStage;

        stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
        stage.setResizable(false);
        stage.setTitle("Application");
        contestLoader = new FXMLLoader(getClass().getResource("/GUI/application.fxml"));
        scene = contestLoader.load();
        stage.setScene(new Scene(scene, 800, 600));
        contestGUIController = contestLoader.getController();
    }

    public void signInButtonClicked() {
        String name = nameTextField.getText();
        String pass = passwordTextField.getText();
        try {
            userController.logIn(name, pass);
            contestGUIController.initComponents(userController, mainStage, stage);
            mainStage.hide();
            stage.setOnCloseRequest((el)->{
                try{
                    userController.logout();
                } catch (Exception e) {
                    //ignored
                }
            });
            stage.show();
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.ERROR, e.getMessage());
            alert.showAndWait();
        }
    }
}
