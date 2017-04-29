package GUI;

import Domain.AgeCategory;
import Domain.Participant;
import Domain.Trial;
import Proxy.ProxyClient;
import Utils.Observers.IObserver;
import javafx.beans.property.SimpleListProperty;
import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;
import org.controlsfx.control.CheckComboBox;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by andrei on 2017-04-13.
 */
public class ContestController implements IObserver {
    private ProxyClient client;

    private Stage parentStage;
    private Stage mainStage;

    @FXML
    private TableView<Trial> trialTableView;

    @FXML
    private TableView<AgeCategory> ageCategoryTableView;

    @FXML
    private TableView<Participant> ParticipantsTableView;

    @FXML
    private TableColumn<Participant, String> ChildNameTableColumn;

    @FXML
    private TableColumn<Participant, Integer> ChildAgeTableColumn;

    @FXML
    private TableColumn<Trial, String> trialNameTableColumn;

    @FXML
    private TableColumn<Trial, Integer> trialDifficultyTableColumn;

    @FXML
    private TableColumn<AgeCategory, String> ageCategoryNameTableColumn;

    @FXML
    private TableColumn<AgeCategory, Integer> ageCategoryMinAgeTableColumn;

    @FXML
    private TableColumn<AgeCategory, Integer> ageCategoryMaxAgeTableColumn;

    @FXML
    private TextField registeredCounterTextField;
    @FXML
    private TextField childAgeTextField;
    @FXML
    private TextField childNameTextField;

    @FXML
    private CheckComboBox<String> trialsComboBox;

    @FXML
    private TextField trialNameTextField;

    @FXML
    private TextField ageCategoryTextField;

    public ContestController() {

    }

    public void initComponents(ProxyClient controller,
                               Stage parentStage,
                               Stage mainStage) throws Exception {
        this.client = controller;
        this.parentStage = parentStage;
        this.mainStage = mainStage;

        client.addObserver(this);

        trialNameTableColumn.setCellValueFactory(new PropertyValueFactory<Trial, String>("name"));
        trialDifficultyTableColumn.setCellValueFactory(new PropertyValueFactory<Trial, Integer>("difficulty"));
        trialNameTableColumn.setCellValueFactory(new PropertyValueFactory<Trial, String>("name"));

        ageCategoryNameTableColumn.setCellValueFactory(new PropertyValueFactory<AgeCategory, String>("name"));
        ageCategoryMaxAgeTableColumn.setCellValueFactory(new PropertyValueFactory<AgeCategory, Integer>("maxAge"));
        ageCategoryMinAgeTableColumn.setCellValueFactory(new PropertyValueFactory<AgeCategory, Integer>("minAge"));

        ChildNameTableColumn.setCellValueFactory(new PropertyValueFactory<Participant, String>("name"));
        ChildAgeTableColumn.setCellValueFactory(new PropertyValueFactory<Participant, Integer>("age"));

        update();
    }

    private void update() throws Exception {
        updateAgeCategory();
        updateTrials();
        updateCounter();
    }

    private void updateTrials() throws Exception {
        List<Trial> allTrials = client.getTrials();
        trialTableView.getItems().clear();
        trialTableView.setItems(new SimpleListProperty<>(FXCollections.observableArrayList(allTrials)));
        trialTableView.getSelectionModel().selectFirst();
        updateComboBox(allTrials);
    }

    private void updateAgeCategory() throws Exception {
        ageCategoryTableView.getItems().clear();
        ageCategoryTableView.setItems(new SimpleListProperty<>(FXCollections.observableArrayList(client.getAgeCategories())));
        ageCategoryTableView.getSelectionModel().selectFirst();
    }

    private void updateCounter() throws Exception {
        try{
            String trialID = trialTableView.getSelectionModel().getSelectedItem().getName();
            String ageCategoryID = ageCategoryTableView.getSelectionModel().getSelectedItem().getName();
            Integer counter = client.getParticipantsForTrial(trialID, ageCategoryID).size();
            registeredCounterTextField.setText(counter.toString());
        }catch (Exception e)
        {
            Alert alert = new Alert(Alert.AlertType.ERROR, e.getMessage());
            alert.showAndWait();
        }
    }

    private void updateComboBox(List<Trial> updatedList) {
        List<String> ids = new ArrayList<>();
        for (Trial t : updatedList) ids.add(t.getId());
        trialsComboBox.getItems().clear();
        trialsComboBox.getItems().addAll(ids);

    }

    public void trialChanged() throws Exception {
        updateCounter();
    }

    public void ageCategoryChanged() throws Exception {
        updateCounter();
    }

    public void searchClickedHandler() throws Exception {
        try {
            String trialID = trialNameTextField.getText();
            String ageCategoryID = ageCategoryTextField.getText();
            List<Participant> participants = client.getParticipantsForTrial(trialID, ageCategoryID);
            ParticipantsTableView.getItems().clear();
            ParticipantsTableView.setItems(new SimpleListProperty<>(FXCollections.observableArrayList(participants)));
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.ERROR, e.getMessage());
            alert.showAndWait();
        }
    }

    public void logoutClickedHandler() throws Exception {
        client.logout();
        mainStage.hide();
        parentStage.show();
    }

    public void registerClickedHandler() throws Exception {
        String childAge = childAgeTextField.getText();
        String childName = childNameTextField.getText();
        List<String> trials = new ArrayList<>(trialsComboBox.getCheckModel().getCheckedItems());
        try {
            client.registerParticipant(childName, childAge, trials);
        } catch (Exception e) {
            Alert alert = new Alert(Alert.AlertType.ERROR, e.getMessage());
            alert.showAndWait();
        }
    }

    private void updateCounter(List<String> updTrial, String updAge)
    {
        String ageCategory = ageCategoryTableView.getSelectionModel().getSelectedItem().getName();
        String trial = trialTableView.getSelectionModel().getSelectedItem().getName();
        if (updTrial.contains(trial) && ageCategory.equals(updAge)) {
            Integer i = Integer.parseInt(registeredCounterTextField.getText()) + 1;
            registeredCounterTextField.setText(i.toString());
        }
    }

    @Override
    public void updateRequired(Object... obj) {
        String ageCategoryName = (String) obj[0];
        List<String> trials = (List<String>) obj[1];
        updateCounter(trials, ageCategoryName);
    }

}
