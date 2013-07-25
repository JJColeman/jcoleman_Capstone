/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package OpenSource.opennlp;

import opennlp.tools.cmdline.ArgumentParser;
import opennlp.tools.cmdline.StreamFactoryRegistry;
import opennlp.tools.cmdline.params.DetokenizerParameter;
import opennlp.tools.formats.DetokenizerSampleStreamFactory;
import opennlp.tools.formats.WordTagSampleStreamFactory;
import opennlp.tools.postag.POSSample;
import opennlp.tools.tokenize.TokenSample;
import opennlp.tools.util.ObjectStream;

/**
 * <b>Note:</b> Do not use this class, internal use only!
 */
public class POSToTokenSampleStreamFactory extends DetokenizerSampleStreamFactory<TokenSample> {

  interface Parameters extends WordTagSampleStreamFactory.Parameters, DetokenizerParameter {
  }

  public static void registerFactory() {
    StreamFactoryRegistry.registerFactory(TokenSample.class,
        "pos", new POSToTokenSampleStreamFactory(Parameters.class));
  }

  protected <P> POSToTokenSampleStreamFactory(Class<P> params) {
    super(params);
  }

  public ObjectStream<TokenSample> create(String[] args) {
    Parameters params = ArgumentParser.parse(args, Parameters.class);

    ObjectStream<POSSample> posSampleStream = StreamFactoryRegistry.getFactory(POSSample.class,
        StreamFactoryRegistry.DEFAULT_FORMAT).create(
        ArgumentParser.filter(args, WordTagSampleStreamFactory.Parameters.class));
    return new POSToTokenSampleStream(createDetokenizer(params), posSampleStream);
  }
}
