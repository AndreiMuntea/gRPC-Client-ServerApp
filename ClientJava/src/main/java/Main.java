import Proxy.ProxyClient;
import javafx.application.Application;
import javafx.stage.Stage;
import GUI.GUI;

/**
 * Created by andrei on 2017-04-28.
 */
public class Main extends Application {
    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        ProxyClient proxyClient = new ProxyClient("localhost",50052);
        GUI gui = new GUI(primaryStage, proxyClient);
        gui.start();
    }
}
