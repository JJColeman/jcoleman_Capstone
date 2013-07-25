/*
 * Copyright 2013 The Apache Software Foundation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package OpenSource.opennlp;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * Properties wrapper for the EntityLinker framework

 */
public class EntityLinkerProperties {

  private Properties props;
  private InputStream stream;
  private String propertyFileLocation = "";

  /**
   * Constructor takes location of properties file as arg
   *
   * @param propertiesfile the location of the properties file
   */
  public EntityLinkerProperties(File propertiesfile) throws IOException, FileNotFoundException {

    props = new Properties();    
    stream = new FileInputStream(propertiesfile);
    props.load(stream);
  }

  public EntityLinkerProperties() {
  }

  /**
   * sets where the props file is without using overloaded constructor
   *
   * @param propertyFileLocation
   */
  public void setPropertyFileLocation(String propertyFileLocation) {

    this.propertyFileLocation = propertyFileLocation;
  }

  /**
   * Gets a property from the props file.
   *
   * @param key the key to the desired item in the properties file (key=value)
   * @param defaultValue a default value in case the file, key, or the value are
   * missing
   * @return
   * @throws FileNotFoundException
   * @throws IOException
   */
  public String getProperty(String key, String defaultValue) throws FileNotFoundException, IOException {
    if (propertyFileLocation == null) {
      throw new FileNotFoundException("property file location not set. Use method setPropertyFileLocation to specify location of entitylinker.properties file, or use constructor and pass in a File.");
    }
    String propVal = defaultValue;
    if (props == null) {
      props = new Properties();
      stream = new FileInputStream(propertyFileLocation);
      props.load(stream);
      stream.close();
    }
    if (props != null) {
      propVal = props.getProperty(key, defaultValue);
    }    
    return propVal;
  }
}