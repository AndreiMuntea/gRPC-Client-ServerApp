package GUI;

import Proxy.ProxyClient;
import javafx.application.Platform;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

/**
 * Created by andrei on 2017-04-13.
 */
public class GUI {
    private Stage mainStage;
    private Parent mainScene;
    private FXMLLoader GUILoader;
    private LoginController loginController;

    public GUI(Stage mainStage, ProxyClient clientController) throws Exception
    {
        this.mainStage = mainStage;
        GUILoader = new FXMLLoader(getClass().getResource("/GUI/login.fxml"));
        mainScene = GUILoader.load();
        loginController = GUILoader.getController();
        loginController.initialiseComponents(clientController, mainStage);

    }

    public void start(){
        mainStage.setTitle("Application");
        mainStage.setScene(new Scene(mainScene, 300, 300));
        mainStage.setResizable(false);
        mainStage.show();
        mainStage.setOnCloseRequest(e -> Platform.exit());
    }
}
